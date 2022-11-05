using FluentValidation;
using PersonnelManagement.Contracts.v1.Requests;

namespace PersonnelManagement.Server.Validators.DepartmentEndpointsValidators
{
    public class CreateDepartmentRequestValidator : AbstractValidator<CreateDepartmentRequest>
    {
        public CreateDepartmentRequestValidator()
        {
            RuleFor(x => x.DepartmentTitle)
                .NotEmpty()
                .MaximumLength(250)
                .MinimumLength(5);

            RuleFor(x => x.DepartmentDescription)
                .NotEmpty()
                .MaximumLength(250)
                .MinimumLength(5);

            RuleFor(x => x.DateFrom)
                .NotEmpty();
        }
    }
}
