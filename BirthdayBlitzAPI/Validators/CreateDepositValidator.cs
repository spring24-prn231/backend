using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Interfaces;
using BusinessObjects.Requests;
using FluentValidation;

namespace BirthdayBlitzAPI.Validators
{
    public class CreateDepositValidator : AbstractValidator<CreateDepositRequest>
    {
        private readonly IOrderService _orderService;
        public CreateDepositValidator(IOrderService orderService)
        {
            _orderService = orderService;
            RuleFor(x => x.Value).GreaterThanOrEqualTo(0)
                .WithMessage("Tiền đặt cọc phải lớn hơn 0đ")
                .PrecisionScale(1, 20, false)
                .WithMessage("Tiền đặt cọc không hợp lệ");
            RuleFor(x => x.OrderId).MustAsync(async (x, cancellationToken) => await _orderService.GetByIdNoTracking(x) != null)
                .WithMessage("Order không tồn tại");
        }
    }
}