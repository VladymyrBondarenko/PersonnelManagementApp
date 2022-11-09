using FluentValidation;
using PersonnelManagement.Contracts.v1.Requests.Identity;

namespace PersonnelManagement.Server.Validators.IdentityEndpointsValidators
{
    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty();

            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
