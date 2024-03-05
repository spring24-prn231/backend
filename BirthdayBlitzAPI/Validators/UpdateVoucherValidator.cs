using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Requests;
using FluentValidation;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateVoucherValidator : AbstractValidator<UpdateVoucherRequest>
    {
        public UpdateVoucherValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code không được để trống");
            RuleFor(x => x.Discount).GreaterThanOrEqualTo(0).WithMessage("Discount phải lớn hơn 0đ");
            RuleFor(x => x.MaximumValue).GreaterThanOrEqualTo(0).WithMessage("MaximumValue phải lớn hơn 0đ");
            RuleFor(x => x.ExpirationDate).GreaterThan(DateTime.Now).WithMessage("ExpirationDate phải lớn hơn ngày hiện tại");
        }
    }
}