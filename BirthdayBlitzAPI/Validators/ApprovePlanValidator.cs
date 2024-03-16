using BusinessObjects.Common.Constants;
using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class ApprovePlanValidator : AbstractValidator<ApprovePlanRequest>
    {
        private readonly IOrderService _orderService;
        public ApprovePlanValidator(IOrderService orderService)
        {
            _orderService = orderService;
            RuleFor(x => x.OrderId)
                .MustAsync(async (x, cancellationToken) =>
                {
                    var order = await _orderService.GetByIdNoTracking(x);
                    if (order == null) return false;
                    return order.ExecutionStatus == (int) OrderStatus.ASSIGNED && order.StaffId != null;
                })
                .WithMessage("Order không tồn tại hoặc trạng thái order không phù hợp!");
        }
    }
}
