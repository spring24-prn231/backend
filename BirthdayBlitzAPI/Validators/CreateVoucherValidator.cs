using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreateVoucherValidator : AbstractValidator<CreateVoucherRequest>
    {
        private readonly IOrderService _orderService;
        public CreateVoucherValidator(IOrderService orderService)
        {
            _orderService = orderService;
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Code không được để trống")
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
                .PrecisionScale(1, 20, false)
                .WithMessage("MaximumValue không hợp lệ, chỉ được chứa tối đa 1 chữ số sau dấu thập phân");
            RuleFor(x => x.ExpirationDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("ExpirationDate phải lớn hơn ngày hiện tại");
            RuleFor(x => x.OrderId).MustAsync(async (x, cancellationToken) => await _orderService.GetByIdNoTracking(x) != null)
                .WithMessage("Order không tồn tại");
        }
    }
}