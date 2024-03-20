using BusinessObjects.Common.Extensions;
using BusinessObjects.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdatePartyPlanValidator : AbstractValidator<UpdatePartyPlanRequest>
    {
        private readonly IPartyPlanService _partyPlanService;
        public UpdatePartyPlanValidator(IPartyPlanService partyPlanService)
        {
            _partyPlanService = partyPlanService;

            RuleFor(x => new { x.TimeStart, x.TimeEnd, x.Id})
                .MustAsync(async (x, cancellationToken) => await ValidateTimeRange(x.TimeStart, x.TimeEnd, x.Id))
                .WithMessage("Khoảng thời gian không hợp lệ");
        }
        private async Task<bool> ValidateTimeRange(DateTime? timeStart, DateTime? timeEnd, Guid id)
        {
            var orderId = await _partyPlanService.GetAll().Where(x => x.Id == id).Select(x => x.OrderId).FirstOrDefaultAsync();
            if (orderId == null) return true;
            if (timeStart == null && timeEnd == null) return true;
            return (!await _partyPlanService.GetAll().GetQueryStatusTrue().AnyAsync(x => x.OrderId == orderId && timeStart < x.TimeEnd && x.TimeStart < timeEnd && x.Id != id)) && timeStart < timeEnd;
        }
    }
}
