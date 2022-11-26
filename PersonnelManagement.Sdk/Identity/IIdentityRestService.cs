using PersonnelManagement.Contracts.v1.Requests.Identity;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Identity;
using PersonnelManagement.Contracts.v1.Routes;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Sdk.Identity
{
    public interface IIdentityRestService
    {
        [Post($"/{ApiRoutes.Identity.Register}")]
        Task<ApiResponse<Response<AuthSuccessResponse>>> RegisterAsync(UserRegistrationRequest registrationRequest);

        [Post($"/{ApiRoutes.Identity.Login}")]
        Task<ApiResponse<Response<AuthSuccessResponse>>> LoginAsync(UserLoginRequest registrationRequest);

        [Post($"/{ApiRoutes.Identity.Refresh}")]
        Task<ApiResponse<Response<AuthSuccessResponse>>> RefreshAsync(RefreshTokenRequest registrationRequest);
    }
}
