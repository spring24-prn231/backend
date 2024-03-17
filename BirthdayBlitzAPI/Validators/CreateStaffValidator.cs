using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Common.Constants;
using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreateStaffValidator : AbstractValidator<CreateStaffRequest>
    {
        public CreateStaffValidator()
        {
            RuleFor(x => x.Role)
                .Must(x => x == UserRole.HOST_STAFF.ToString() || x == UserRole.IMPLEMENT_STAFF.ToString())
                .WithMessage("Role không hợp lệ");
        }
    }
}