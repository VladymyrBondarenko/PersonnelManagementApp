using FluentValidation;
using PersonnelManagement.Contracts.v1.Requests.Positions;

namespace PersonnelManagement.Server.Validators.PositionEndpointsValidators
{
    public class UpdatePositionRequestValidator : AbstractValidator<UpdatePositionRequest>
    {
        public UpdatePositionRequestValidator()
        {
            RuleFor(x => x.PositionTitle)
                .NotEmpty()
                .MaximumLength(250)
                .MinimumLength(5);

            RuleFor(x => x.PositionDescription)
                .NotEmpty()
                .MaximumLength(250)
                .MinimumLength(5);
        }
    }
}
