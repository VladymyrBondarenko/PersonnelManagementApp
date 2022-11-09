using FluentValidation;
using PersonnelManagement.Contracts.v1.Requests.Identity;

namespace PersonnelManagement.Server.Validators.IdentityEndpointsValidators
{
    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginRequestValidator()
        {
            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
