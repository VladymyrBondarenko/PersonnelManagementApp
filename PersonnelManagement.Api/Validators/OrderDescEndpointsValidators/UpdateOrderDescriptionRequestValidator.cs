using FluentValidation;
using PersonnelManagement.Contracts.v1.Requests.Orders;

namespace PersonnelManagement.Server.Validators.OrderDescEndpointsValidators
{
    public class UpdateOrderDescriptionRequestValidator : AbstractValidator<UpdateOrderDescriptionRequest>
    {
        public UpdateOrderDescriptionRequestValidator()
        {
            RuleFor(x => x.OrderDescriptionTitle)
                .NotEmpty()
                .MaximumLength(250)
                .MinimumLength(5);
        }
    }
}
