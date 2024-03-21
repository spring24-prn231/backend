using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateServiceValidator : AbstractValidator<UpdateServiceRequest>
    {
        private readonly IServiceElementService _serviceElementService;
        private readonly IMenuService _menuService;
        private readonly IDishService _dishService;
        public UpdateServiceValidator(IServiceElementService serviceElementService, IMenuService menuService, IDishService dishService)
        {
            _serviceElementService = serviceElementService;
            _menuService = menuService;
            _dishService = dishService;
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("Tên dịch vụ không được quá 100 ký tự");
            RuleFor(x => x.Description)
                .MaximumLength(255)
                .WithMessage("Mô tả dịch vụ không được quá 255 kýtự");
            RuleForEach(x => x.ServiceElementIds)
                .ChildRules(child =>
                {
                    child.RuleFor(x => x)
                        .MustAsync(async (y, cancellationToken) => await _serviceElementService.GetByIdNoTracking(y) != null)
                        .WithMessage("Dịch vụ không tồn tại");
                });
            RuleForEach(x => x.DishIds)
                .ChildRules(child =>
                {
                    child.RuleFor(x => x)
                        .MustAsync(async (y, cancellationToken) => await _dishService.GetByIdNoTracking(y) != null)
                        .WithMessage("Món ăn không tồn tại trong Menu");
                });

            RuleFor(x => x.DishIds)
                .Must(dishes =>
                {
                    foreach (var dish in dishes)
                    {
                        if (dishes.Where(x => x == dish).Count() > 1) return false;
                    }
                    return true;
                })
                .WithMessage("Không được add các món ăn trùng nhau");
            RuleFor(x => x.ServiceElementIds)
                .Must(elements =>
                {
                    foreach (var element in elements)
                    {
                        if (elements.Where(x => x == element).Count() > 1) return false;
                    }
                    return true;
                })
                .WithMessage("Không được add các dịch vụ trùng nhau");
        }
    }
}