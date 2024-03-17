using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Requests;
using FluentValidation;
using Services.Implements;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreateServiceValidator : AbstractValidator<CreateServiceRequest>
    {
        private readonly IRoomTypeService _roomTypeService;
        private readonly IServiceElementService _serviceElementService;
        private readonly IMenuService _menuService;
        private readonly IDishService _dishService;
        public CreateServiceValidator(IRoomTypeService roomTypeService, IServiceElementService serviceElementService, IMenuService menuService, IDishService dishService)
        {
            _roomTypeService = roomTypeService;
            _menuService = menuService;
            _serviceElementService = serviceElementService;
            _dishService = dishService;
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("Tên dịch vụ không được quá 100 ký tự");
            RuleFor(x => x.Description)
                .MaximumLength(255)
                .WithMessage("Mô tả dịch vụ không được quá 255 ký tự");
            RuleFor(x => x.RoomTypeId).MustAsync(async (x, cancellationToken) => await _roomTypeService.GetByIdNoTracking(x) != null)
                .WithMessage("Loại phòng không tồn tại");
            RuleForEach(x => x.ServiceElementIds)
                .ChildRules(child => {
                    child.RuleFor(x => x)
                        .MustAsync(async (y, cancellationToken) => await _serviceElementService.GetByIdNoTracking(y) != null)
                        .WithMessage("ServiceElement không tồn tại");
                });
            RuleForEach(x => x.DishIds)
                .ChildRules(child => {
                    child.RuleFor(x => x)
                        .MustAsync(async (y, cancellationToken) => await _dishService.GetByIdNoTracking(y) != null)
                        .WithMessage("Món ăn không tồn tại trong Menu");
                });
        }
    }
}