using BusinessObjects.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdatePartyPlanListValidator : AbstractValidator<List<UpdatePartyPlanRequest>>
    {

        public UpdatePartyPlanListValidator(IPartyPlanService partyPlanService)
        {

            var updatePartyPlanValidator = new UpdatePartyPlanValidator(partyPlanService);

            RuleForEach(x => x)
                .SetValidator(updatePartyPlanValidator);
        }
    }
}