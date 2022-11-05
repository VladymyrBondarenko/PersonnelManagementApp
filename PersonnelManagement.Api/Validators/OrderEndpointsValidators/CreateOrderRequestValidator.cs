using FluentValidation;
using PersonnelManagement.Contracts.v1.Requests.Orders;

namespace PersonnelManagement.Server.Validators.OrderEndpointsValidators
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(250)
                .MinimumLength(3);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(250)
                .MinimumLength(3);

            RuleFor(x => x.OrderDescriptionId)
                .NotEmpty();

            RuleFor(x => x.DateFrom)
                .NotEmpty();
        }
    }
}
