using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Application.Identities;
using PersonnelManagement.Contracts.v1.Requests.Identity;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Identity;
using PersonnelManagement.Contracts.v1.Routes;
using PersonnelManagement.Domain.Models.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonnelManagement.Server.Controllers.v1
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public IdentityController(IIdentityService identityService, IMapper mapper)
        {
            _identityService = identityService;
            _mapper = mapper;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            var registractionQuery = _mapper.Map<UserRegistrationQuery>(request);
            var res = await _identityService.RegisterAsync(registractionQuery);

            if (!res.Success)
            {
                return BadRequest(new ErrorResponse 
                { 
                    Errors = res.Errors.Select(x => new ErrorModel { Message = x }).ToList()
                });
            }

            return Ok(new Response<AuthSuccessResponse>(
                new AuthSuccessResponse 
                { 
                    Token = res.Token, 
                    RefreshToken = res.RefreshToken 
                }));
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var res = await _identityService.LoginAsync(request.Email, request.Password);

            if (!res.Success)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = res.Errors.Select(x => new ErrorModel { Message = x }).ToList()
                });
            }
            ;
            return Ok(new Response<AuthSuccessResponse>(
                new AuthSuccessResponse 
                { 
                    Token = res.Token, 
                    RefreshToken = res.RefreshToken 
                }));
        }

        [HttpPost(ApiRoutes.Identity.Refresh)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var res = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!res.Success)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = res.Errors.Select(x => new ErrorModel { Message = x }).ToList()
                });
            }

            return Ok(new Response<AuthSuccessResponse>(
                new AuthSuccessResponse 
                { 
                    Token = res.Token, 
                    RefreshToken = res.RefreshToken 
                }));
        }
    }
}
