using FluentValidation;
using PersonnelManagement.Contracts.v1.Requests.Employees;

namespace PersonnelManagement.Server.Validators.EmployeeEndpointsValidators
{
    public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
    {
        public CreateEmployeeRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(250)
                .MinimumLength(3);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(250)
                .MinimumLength(3);

            RuleFor(x => x.HireDate)
                .NotEmpty();
        }
    }
}
