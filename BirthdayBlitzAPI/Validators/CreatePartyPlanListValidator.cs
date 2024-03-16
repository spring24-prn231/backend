using BusinessObjects.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreatePartyPlanListValidator : AbstractValidator<List<CreatePartyPlanRequest>>
    {
        private readonly IPartyPlanService _partyPlanService;
        private readonly IOrderService _orderService;

        public CreatePartyPlanListValidator(IPartyPlanService partyPlanService, IOrderService orderService)
        {
            _partyPlanService = partyPlanService;
            _orderService = orderService;

            var createPartyPlanValidator = new CreatePartyPlanValidator(partyPlanService, orderService);

            RuleForEach(x => x)
                .SetValidator(createPartyPlanValidator);
        }
    }
}