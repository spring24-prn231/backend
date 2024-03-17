using BusinessObjects.Common.Constants;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class AssignStaffPlanValidator : AbstractValidator<AssignStaffPlanRequest>
    {
        private readonly IPartyPlanService _partyPlanService;
        private readonly UserManager<ApplicationUser> _userManager;
        public AssignStaffPlanValidator(IPartyPlanService partyPlanService, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _partyPlanService = partyPlanService;
            RuleFor(x => x.PlanId)
                .MustAsync(async (x, cancellationToken) => await _partyPlanService.GetById(x) != null)
                .WithMessage("Plan Không tồn tại");
            RuleForEach(x => x.StaffIds)
                .MustAsync(async (x, cancellationToken) =>
                {
                    var staff = await _userManager.FindByIdAsync(x.ToString());
                    return staff != null && await _userManager.IsInRoleAsync(staff, UserRole.IMPLEMENT_STAFF.ToString());
                })
                .WithMessage("Staff không tồn lại hoặc Role không hợp lệ");
        }
    }
}
