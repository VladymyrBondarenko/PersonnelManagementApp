using PersonnelManagement.Domain.Models.Identity;

namespace PersonnelManagement.Application.Identities
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string token, Guid refreshToken);
        Task<AuthenticationResult> RegisterAsync(UserRegistrationQuery userRegistrationQuery);
    }
}