using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateStaffValidator : AbstractValidator<UpdateStaffRequest>
    {
        public UpdateStaffValidator()
        {
            RuleFor(x => x.Role)
                .Must(x => x == null || x == "HOST_STAFF" || x == "IMPLEMENT_STAFF")
                .WithMessage("Role không hợp lệ");
        }
    }
}