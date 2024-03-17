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
            RuleFor(x => x.Code)
                .MaximumLength(16)
                .WithMessage("Code không được vượt quá 16 ký tự")
                .Matches("^[a-zA-Z0-9]*$")
                .WithMessage("Code chỉ được chứa ký tự và số");
            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Discount phải lớn hơn 0đ");
            RuleFor(x => x.MaximumValue)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MaximumValue phải lớn hơn 0đ")
                .PrecisionScale(20, 1, false)
                .WithMessage("MaximumValue không hợp lệ, chỉ được chứa tối đa 1 chữ số sau dấu thập phân");
            RuleFor(x => x.ExpirationDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("ExpirationDate phải lớn hơn ngày hiện tại");
        }
    }
}