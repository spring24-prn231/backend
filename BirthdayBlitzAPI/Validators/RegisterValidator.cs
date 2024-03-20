using BusinessObjects.Models;
using BusinessObjects.Requests.Authentication;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BirthdayBlitzAPI.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public RegisterValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            RuleFor(x => x.Email).EmailAddress()
                .MustAsync(async (x, cancellationToken) => await _userManager.FindByEmailAsync(x) == null)
                .WithMessage("Email đã tồn tại");
            RuleFor(x => x.PhoneNumber)
                .MustAsync(async (x, cancellationToken) => await _userManager.Users.Where(i => i.PhoneNumber == x).FirstOrDefaultAsync() == null)
                .WithMessage("Số điện thoại đã tồn tại")
                .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("Số điện thoại không hợp lệ!");
            RuleFor(x => x.Username)
                .MustAsync(async (x, cancellationToken) => await _userManager.FindByNameAsync(x) == null)
                .WithMessage("Username đã tồn tại");

        }
    }
}
