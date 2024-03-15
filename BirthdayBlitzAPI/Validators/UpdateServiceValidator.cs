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
        public UpdateServiceValidator(IServiceElementService serviceElementService, IMenuService menuService)
        {
            _serviceElementService = serviceElementService;
            _menuService = menuService;
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("Tên dịch vụ không được quá 100 ký tự");
            RuleFor(x => x.Description)
                .MaximumLength(255)
                .WithMessage("Mô tả dịch vụ không được quá 255 kýtự");
            RuleForEach(x => x.ServiceElementIds)
                .ChildRules(child => {
                    child.RuleFor(x => x)
                        .MustAsync(async (y, cancellationToken) => await _serviceElementService.GetByIdNoTracking(y) != null)
                        .WithMessage("ServiceElement không tồn tại");
                });
            RuleForEach(x => x.DishIds)
                .ChildRules(child => {
                    child.RuleFor(x => x)
                        .MustAsync(async (y, cancellationToken) => await _menuService.GetByIdNoTracking(y) != null)
                        .WithMessage("Món ăn không tồn tại trong Menu");
                });
        }
    }
}