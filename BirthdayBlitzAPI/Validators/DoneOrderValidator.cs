using BusinessObjects.Common.Constants;
using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class DoneOrderValidator : AbstractValidator<DoneOrderRequest>
    {
        private readonly IOrderService _orderService;
        public DoneOrderValidator(IOrderService orderService)
        {
            _orderService = orderService;
            RuleFor(x => x.OrderId)
                .MustAsync(async (x, cancellationToken) =>
                {
                    var order = await _orderService.GetByIdNoTracking(x);
                    if (order == null) return false;
                    return order.ExecutionStatus == (int)OrderStatus.EXECUTING;
                })
                .WithMessage("Order không tồn tại hoặc trạng thái order không phù hợp!");
        }
    }
}
