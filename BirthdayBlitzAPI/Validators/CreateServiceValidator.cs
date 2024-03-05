using System;
using System.Collections.Generic;
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
        public CreateServiceValidator(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .WithMessage("Tên dịch vụ không được quá 100 ký tự");
            RuleFor(x => x.Description)
                .MaximumLength(255)
                .WithMessage("Mô tả dịch vụ không được quá 255 ký tự");
            RuleFor(x => x.RoomTypeId).Must(x => _roomTypeService.GetByIdNoTracking(x) != null)
                .WithMessage("Loại phòng không tồn tại");
        }
    }
}