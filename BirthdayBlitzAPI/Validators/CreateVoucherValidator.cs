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
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code không được để trống");
            RuleFor(x => x.Discount).GreaterThanOrEqualTo(0).WithMessage("Discount phải lớn hơn 0đ");
            RuleFor(x => x.MaximumValue).GreaterThanOrEqualTo(0).WithMessage("MaximumValue phải lớn hơn 0đ");
            RuleFor(x => x.ExpirationDate).GreaterThan(DateTime.Now).WithMessage("ExpirationDate phải lớn hơn ngày hiện tại");
            RuleFor(x => x.OrderId).Must(x => _orderService.GetByIdNoTracking(x) != null)
                .WithMessage("Order không tồn tại");
        }
    }
}