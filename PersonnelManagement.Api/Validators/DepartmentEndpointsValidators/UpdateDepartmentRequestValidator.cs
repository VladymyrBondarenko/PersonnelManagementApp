using FluentValidation;
using PersonnelManagement.Contracts.v1.Responses.Departments;

namespace PersonnelManagement.Server.Validators.DepartmentEndpointsValidators
{
    public class UpdateDepartmentRequestValidator : AbstractValidator<UpdateDepartmentRequest>
    {
        public UpdateDepartmentRequestValidator()
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
