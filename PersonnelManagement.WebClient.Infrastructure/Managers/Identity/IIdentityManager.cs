using PersonnelManagement.Contracts.v1.Requests.Identity;
using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Contracts.v1.Responses.Identity;
using Refit;

namespace PersonnelManagement.WebClient.Infrastructure.Managers.Identity
{
    public interface IIdentityManager
    {
        Task<Response<AuthSuccessResponse>> LoginAsync(UserLoginRequest registrationRequest);
        Task LogoutAsync();
        Task<Response<AuthSuccessResponse>> RefreshAsync(RefreshTokenRequest request);
        Task<Response<AuthSuccessResponse>> RegisterAsync(UserRegistrationRequest registrationRequest);
    }
}