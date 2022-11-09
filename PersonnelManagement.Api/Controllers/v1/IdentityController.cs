using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.Application.Identities;
using PersonnelManagement.Contracts.v1.Requests.Identity;
using PersonnelManagement.Contracts.v1.Responses.Identity;
using PersonnelManagement.Contracts.v1.Routes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonnelManagement.Server.Controllers.v1
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            var res = await _identityService.RegisterAsync(request.Email, request.Password);

            if (!res.Success)
            {
                return BadRequest(new AuthFailedResponse { Errors = res.Errors });
            }

            return Ok(new AuthSuccessResponse { Token = res.Token, RefreshToken = res.RefreshToken });
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var res = await _identityService.LoginAsync(request.Email, request.Password);

            if (!res.Success)
            {
                return BadRequest(new AuthFailedResponse { Errors = res.Errors });
            }

            return Ok(new AuthSuccessResponse { Token = res.Token, RefreshToken = res.RefreshToken });
        }

        [HttpPost(ApiRoutes.Identity.Refresh)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var res = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!res.Success)
            {
                return BadRequest(new AuthFailedResponse { Errors = res.Errors });
            }

            return Ok(new AuthSuccessResponse { Token = res.Token, RefreshToken = res.RefreshToken });
        }
    }
}
