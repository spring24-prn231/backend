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
    public class CreateServiceValidator : AbstractValidator<CreateServiceRequest>
    {
        private readonly IRoomTypeService _roomTypeService;
        private readonly IServiceElementDetailService _serviceElementDetailService;
        private readonly IMenuService _menuService;
        public CreateServiceValidator(IRoomTypeService roomTypeService, IServiceElementDetailService serviceElementDetailService, IMenuService menuService)
        {
            _roomTypeService = roomTypeService;
            _serviceElementDetailService = serviceElementDetailService;
            _menuService = menuService;
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("Tên dịch vụ không được quá 100 ký tự");
            RuleFor(x => x.Description)
                .MaximumLength(255)
                .WithMessage("Mô tả dịch vụ không được quá 255 ký tự");
            RuleFor(x => x.RoomTypeId).Must(x => _roomTypeService.GetByIdNoTracking(x) != null)
                .WithMessage("Loại phòng không tồn tại");
            RuleFor(x => x.ServiceElementIds)
                .ChildRules(child => {
                    child.RuleFor(x => x)
                        .Must(x => x.All(y => _serviceElementDetailService.GetByIdNoTracking(y) != null))
                        .WithMessage("ServiceElement không tồn tại");
                });
            RuleFor(x => x.DishIds)
                .ChildRules(child => {
                    child.RuleFor(x => x)
                        .Must(x => x.All(y => _menuService.GetByIdNoTracking(y) != null))
                        .WithMessage("Món ăn không tồn tại trong Menu");
                });
        }
    }
}