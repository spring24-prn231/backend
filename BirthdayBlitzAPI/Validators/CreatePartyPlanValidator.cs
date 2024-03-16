using BusinessObjects.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services.Implements;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreatePartyPlanValidator : AbstractValidator<CreatePartyPlanRequest>
    {
        private readonly IPartyPlanService _partyPlanService;
        private readonly IOrderService _orderService;
        public CreatePartyPlanValidator(IPartyPlanService partyPlanService, IOrderService orderService)
        {
            _partyPlanService = partyPlanService;
            _orderService = orderService;

            //Update Later by Khanh-lof
            RuleFor(x => x.OrderId).MustAsync(async (x, cancellationToken) => await _orderService.GetByIdNoTracking(x) != null)
                .WithMessage("Order không tồn tại");

            RuleFor(x => new { x.TimeStart, x.TimeEnd, x.OrderId })
                .MustAsync(async (x, cancellationToken) => await ValidateTimeRange(x.TimeStart, x.TimeEnd, x.OrderId))
                .WithMessage("Khoảng thời gian không hợp lệ");
        }
        private async Task<bool> ValidateTimeRange(DateTime? timeStart, DateTime? timeEnd, Guid orderId)
        {
            if (timeStart == null && timeEnd == null) return true;
            return (!await _partyPlanService.GetAll().AnyAsync(x => x.OrderId == orderId && timeStart < x.TimeEnd && x.TimeStart < timeEnd)) && timeStart < timeEnd;
        }
    }
}