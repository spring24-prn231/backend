using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateRoomTypeValidator : AbstractValidator<UpdateRoomTypeRequest>
    {
        private readonly IRoomTypeService _roomTypeService;
        public UpdateRoomTypeValidator(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
            RuleFor(x => x.Id)
                .Must(x => _roomTypeService.GetByIdNoTracking(x) != null)
                .WithMessage("Loại phòng không tồn tại");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Không được để trống loại phòng")
                .MaximumLength(100).WithMessage("Phân loại phòng không quá 100 kí tự");
            RuleFor(x => x.Description)
                .MaximumLength(255).WithMessage("Mô tả không quá 255 kí tự");
        }
    }
}
