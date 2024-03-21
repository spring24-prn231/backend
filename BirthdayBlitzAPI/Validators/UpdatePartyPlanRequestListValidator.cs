using BusinessObjects.Common.Constants;
using BusinessObjects.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services.Implements;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdatePartyPlanRequestListValidator : AbstractValidator<UpdatePartyPlanRequestList>
    {
        private readonly IOrderService _orderService;
        public UpdatePartyPlanRequestListValidator(IOrderService orderService)
        {
            _orderService = orderService;

            RuleFor(x => x.OrderId).MustAsync(async (x, cancellationToken) =>
            {
                var order = await _orderService.GetByIdNoTracking(x);
                return order != null && order.ExecutionStatus == (int)OrderStatus.ASSIGNED;
            })
                .WithMessage("Order không hợp lệ");

            RuleForEach(x => x.PartyPlans).SetValidator(x => new UpdatePartyPlanEachValidator(x.OrderId, x.PartyPlans));

        }
    }
    public class UpdatePartyPlanEachValidator : AbstractValidator<UpdatePartyPlanDetailRequest>
    {
        private Guid? _orderId;
        private List<UpdatePartyPlanDetailRequest>? _partyPlans;
        public UpdatePartyPlanEachValidator(Guid? orderId = null, List<UpdatePartyPlanDetailRequest> partyPlans = null)
        {
            _orderId = orderId;
            _partyPlans = partyPlans;

            RuleFor(x => new { x.TimeStart, x.TimeEnd })
                .Must(x => ValidateTimeRange(x.TimeStart, x.TimeEnd))
                .WithMessage("Khoảng thời gian không hợp lệ");
            RuleFor(x => x.TimeStart)
                .GreaterThanOrEqualTo(x => DateTime.Now).WithMessage("Thời gian bắt đầu phải lớn hơn thời gian hiện tại");
            _partyPlans = partyPlans;
        }
        private bool ValidateTimeRange(DateTime? timeStart, DateTime? timeEnd)
        {
            if (_orderId == null) return true;
            if (timeStart == null && timeEnd == null) return true;
            return (_partyPlans!.Where(x => timeStart < x.TimeEnd && x.TimeStart < timeEnd)).Count() <= 1 && timeStart < timeEnd;
        }
    }
}
