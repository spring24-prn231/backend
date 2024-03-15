using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreateFeedbackValidator : AbstractValidator<CreateFeedbackRequest>
    {
        private readonly IOrderService _orderService;
        public CreateFeedbackValidator(IOrderService orderService)
        {
            _orderService = orderService;
            RuleFor(x => x.RatingStar).LessThanOrEqualTo((byte) 5);
            RuleFor(x => x.OrderId).MustAsync(async (x, cancellationToken) => await _orderService.GetByIdNoTracking(x) != null)
                .WithMessage("Order không tồn tại");
        }
    }
}
