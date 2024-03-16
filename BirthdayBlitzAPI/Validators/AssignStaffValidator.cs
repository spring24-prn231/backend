using BusinessObjects.Common.Constants;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;
using System.Threading;

namespace BirthdayBlitzAPI.Validators
{
    public class AssignStaffValidator : AbstractValidator<AssignStaffRequest>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;
        public AssignStaffValidator(UserManager<ApplicationUser> userManager,
                                    IOrderService orderService)
        {
            _userManager = userManager;
            _orderService = orderService;
            RuleFor(x => x.StaffId)
                .MustAsync(async (x, cancellationToken) =>
                {
                    var staff = await _userManager.FindByIdAsync(x.ToString());
                    if (staff == null) return false;
                    return await _userManager.IsInRoleAsync(staff, UserRole.HOST_STAFF.ToString());
                }).WithMessage("Nhân viên không tồn tại");
            RuleFor(x => x.OrderId)
                .MustAsync(async (x, cancellationToken) =>
                {
                    var order = await _orderService.GetByIdNoTracking(x);
                    if (order == null) return false;
                    return order.ExecutionStatus == (int)OrderStatus.NEW || order.ExecutionStatus == (int)OrderStatus.ASSIGNED;
                })
                .WithMessage("Order không tồn tại hoặc trạng thái order không phù hợp!");
        }
    }
}
