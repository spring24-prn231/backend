using BusinessObjects.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services.Implements;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdatePartyPlanRequestListValidator : AbstractValidator<List<UpdatePartyPlanRequestList>>
    {
        public UpdatePartyPlanRequestListValidator(IPartyPlanService partyPlanService)
        {

            var updatePartyPlanValidator = new UpdatePartyPlanEachValidator(partyPlanService);
            RuleForEach(x => x).SetValidator(updatePartyPlanValidator);
        }
    }
    public class UpdatePartyPlanEachValidator : AbstractValidator<UpdatePartyPlanRequestList>
    {
        private readonly IPartyPlanService _partyPlanService;
        public UpdatePartyPlanEachValidator(IPartyPlanService partyPlanService)
        {
            _partyPlanService = partyPlanService;

            RuleFor(x => new { x.TimeStart, x.TimeEnd, x.Id })
                .MustAsync(async (x, cancellationToken) => await ValidateTimeRange(x.TimeStart, x.TimeEnd, x.Id.Value))
                .WithMessage("Khoảng thời gian không hợp lệ");
        }
        private async Task<bool> ValidateTimeRange(DateTime? timeStart, DateTime? timeEnd, Guid id)
        {
            var orderId = await _partyPlanService.GetAll().Where(x => x.Id == id).Select(x => x.OrderId).FirstOrDefaultAsync();
            if (orderId == null) return true;
            if (timeStart == null && timeEnd == null) return true;
            return (!await _partyPlanService.GetAll().AnyAsync(x => x.OrderId == orderId && timeStart < x.TimeEnd && x.TimeStart < timeEnd)) && timeStart < timeEnd;
        }
    }
}
