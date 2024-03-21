using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BusinessObjects.Common.Constants;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreateStaffValidator : AbstractValidator<CreateStaffRequest>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public CreateStaffValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            RuleFor(x => x.Role)
                .Must(x => x == UserRole.HOST_STAFF.ToString() || x == UserRole.IMPLEMENT_STAFF.ToString())
                .WithMessage("Role không hợp lệ");
            RuleFor(x => x.Email).EmailAddress()
                .MustAsync(async (x, cancellationToken) => await _userManager.FindByEmailAsync(x) == null)
                .WithMessage("Email đã tồn tại");
            RuleFor(x => x.PhoneNumber)
                .MustAsync(async (x, cancellationToken) => await _userManager.Users.Where(i => i.PhoneNumber == x).FirstOrDefaultAsync() == null)
                .WithMessage("Số điện thoại đã tồn tại")
                .Matches(new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")).WithMessage("Số điện thoại không hợp lệ!");
            RuleFor(x => x.Username)
                .MustAsync(async (x, cancellationToken) => await _userManager.FindByNameAsync(x) == null)
                .WithMessage("Username đã tồn tại");
        }
    }
}