using BusinessObjects.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdatePartyPlanListValidator : AbstractValidator<List<UpdatePartyPlanRequest>>
    {
        private readonly IPartyPlanService _partyPlanService;
        private readonly IOrderService _orderService;

        public UpdatePartyPlanListValidator(IPartyPlanService partyPlanService, IOrderService orderService)
        {
            _partyPlanService = partyPlanService;
            _orderService = orderService;

            var updatePartyPlanValidator = new UpdatePartyPlanValidator(partyPlanService, orderService);

            RuleForEach(x => x)
                .SetValidator(updatePartyPlanValidator);
        }
    }
}