using FluentValidation;
using PersonnelManagement.Contracts.v1.Requests.Originals;

namespace PersonnelManagement.Server.Validators.OriginalEndpointsValidators
{
    public class UpdateOriginalRequestValidator : AbstractValidator<UpdateOriginalRequest>
    {
        public UpdateOriginalRequestValidator()
        {
            RuleFor(x => x.OriginalTitle)
                .NotEmpty()
                .MaximumLength(250);
        }
    }
}
