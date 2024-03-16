using BusinessObjects.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdatePartyPlanValidator : AbstractValidator<UpdatePartyPlanRequest>
    {
        private readonly IPartyPlanService _partyPlanService;
        private readonly IOrderService _orderService;
        public UpdatePartyPlanValidator(IPartyPlanService partyPlanService, IOrderService orderService)
        {
            _partyPlanService = partyPlanService;
            _orderService = orderService;

            RuleFor(x => x.TimeStart)
                .MustAsync(async (timeStart, cancellationToken) => !await _partyPlanService.GetAll().AnyAsync(r => r.TimeStart == timeStart, cancellationToken))
                .WithMessage("Thời gian bắt đầu đã tồn tại");
            RuleFor(x => x.TimeEnd)
                .MustAsync(async (timeEnd, cancellationToken) => !await _partyPlanService.GetAll().AnyAsync(r => r.TimeEnd == timeEnd, cancellationToken))
                .WithMessage("Thời gian kết thúc đã tồn tại");
            RuleFor(x => x.TimeStart)
                .LessThan(x => x.TimeEnd)
                .WithMessage("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc");
        }
    }
}
