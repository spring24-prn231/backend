using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Requests;
using FluentValidation;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateDepositValidator : AbstractValidator<UpdateDepositRequest>
    {
        public UpdateDepositValidator()
        {
            RuleFor(x => x.Value).GreaterThanOrEqualTo(0)
                .WithMessage("Tiền đặt cọc phải lớn hơn 0đ");
        }
    }
}