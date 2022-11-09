using FluentValidation;
using PersonnelManagement.Contracts.v1.Requests.Identity;

namespace PersonnelManagement.Server.Validators.IdentityEndpointsValidators
{
    public class UserRegistrationRequestValidator : AbstractValidator<UserRegistrationRequest>
    {
        public UserRegistrationRequestValidator()
        {
           RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
