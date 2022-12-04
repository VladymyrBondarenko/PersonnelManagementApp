using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.Identities;
using PersonnelManagement.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Infrastracture.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUserModel> _userManager;
        private readonly JwtSettingsOptions _jwtSettings;
        private readonly TokenValidationParameters _tokenParams;
        private readonly IApplicationDbContext _dbContext;

        public IdentityService(
            UserManager<IdentityUserModel> userManager, JwtSettingsOptions jwtSettings,
            TokenValidationParameters tokenParams, IApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _tokenParams = tokenParams;
            _dbContext = dbContext;
        }

        public async Task<AuthenticationResult> RegisterAsync(UserRegistrationQuery userRegistrationQuery)
        {
            var existingUser = await _userManager.FindByEmailAsync(userRegistrationQuery.Email);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { $"User with email '{userRegistrationQuery.Email}' already exists. Try to use another one." }
                };
            }

            var user = new IdentityUserModel
            {
                Email = userRegistrationQuery.Email,
                UserName = userRegistrationQuery.UserName,
                FirstName = userRegistrationQuery.FirstName,
                LastName = userRegistrationQuery.LastName,
                PhoneNumber = userRegistrationQuery.PhoneNumber
            };

            var createdUser = await _userManager.CreateAsync(user, userRegistrationQuery.Password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            return await generateAuthResultAsync(user);
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { $"User doest not exist." }
                };
            }

            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Password is not correct." }
                };
            }

            return await generateAuthResultAsync(user);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, Guid refreshToken)
        {
            var validatedToken = getPrincipalFromToken(token);

            if (validatedToken == null)
            {
                return new AuthenticationResult { Errors = new[] { "Invalid Token" } };
            }

            var expiryDateUnix =
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expityDateUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expityDateUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult { Errors = new[] { "This token hasn't expired yet" } };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _dbContext.RefreshTokens.FindAsync(refreshToken);

            if (storedRefreshToken == null ||
                DateTime.UtcNow > expityDateUtc ||
                storedRefreshToken.Invalidated ||
                storedRefreshToken.IsUsed ||
                storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has been invalidated" } };
            }

            storedRefreshToken.IsUsed = true;

            _dbContext.RefreshTokens.Update(storedRefreshToken);
            await _dbContext.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);

            return await generateAuthResultAsync(user);
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters 
            {
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret))
            };

            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private ClaimsPrincipal getPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenParams, out SecurityToken validateToken);

                if (!isJwtValidSecurityAlgorithm(validateToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool isJwtValidSecurityAlgorithm(SecurityToken token)
        {
            return token is JwtSecurityToken securityToken &&
                securityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task<AuthenticationResult> generateAuthResultAsync(IdentityUser identity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, identity.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, identity.Email),
                    new Claim("id", identity.Id),
                }),
                Expires = DateTime.UtcNow.AddSeconds(_jwtSettings.TokenLifeTime.Seconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = identity.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _dbContext.RefreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();

            var res = new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token.ToString()
            };

            return res;
        }
    }
}
