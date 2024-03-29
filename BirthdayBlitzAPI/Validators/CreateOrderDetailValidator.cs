﻿using BusinessObjects.Requests;
using FluentValidation;
using Services.Implements;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreateOrderDetailValidator : AbstractValidator<CreateOrderDetailRequest>
    {
        private readonly IOrderService _orderService;

        public CreateOrderDetailValidator(IOrderService orderService)
        {
            _orderService = orderService;
            RuleFor(x => x.OrderId)
               .MustAsync(async (x, cancellationToken) => await _orderService.GetByIdNoTracking(x) != null)
               .WithMessage("Id của đơn hàng này không tồn tại");
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Giá tiền phải lớn hơn 0")
                .GreaterThanOrEqualTo(x => x.Cost)
                .WithMessage("Giá tiền phải lớn hơn hoặc bằng nhà vốn");
            RuleFor(x => x.Cost)
                .GreaterThan(0)
                .WithMessage("Tiền phải lớn hơn 0");
            RuleFor(x => x.Note)
                .MaximumLength(255)
                .WithMessage("Ghi chú về chi tiết đơn hàng không vượt quá 255 kí tự");
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Số lượng trong chi tiết đơn hàng phải lớn hơn 0");
            RuleFor(x => x.Type)
                .MaximumLength(255)
                .WithMessage("Loại của chi tiết đơn hàng không vượt quá 255 kí tự");
        }
    }
}