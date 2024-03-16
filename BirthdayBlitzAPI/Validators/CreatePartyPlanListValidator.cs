using BusinessObjects.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreatePartyPlanListValidator : AbstractValidator<List<CreatePartyPlanRequest>>
    {
        private readonly IPartyPlanService _partyPlanService;

        public CreatePartyPlanListValidator(IPartyPlanService partyPlanService)
        {
            _partyPlanService = partyPlanService;

            RuleForEach(x => x)
                .MustAsync(async (request, cancellationToken) => !await _partyPlanService.GetAll().AnyAsync(r => r.TimeStart == request.TimeStart, cancellationToken))
                .WithMessage("Thời gian bắt đầu đã tồn tại")
                .MustAsync(async (request, cancellationToken) => !await _partyPlanService.GetAll().AnyAsync(r => r.TimeEnd == request.TimeEnd, cancellationToken))
                .WithMessage("Thời gian kết thúc đã tồn tại")
                .Must((request) => request.TimeStart < request.TimeEnd)
                .WithMessage("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc");
        }
    }
}
