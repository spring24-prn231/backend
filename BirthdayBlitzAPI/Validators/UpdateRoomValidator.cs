using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateRoomValidator : AbstractValidator<UpdateRoomRequest>
    {
        private readonly IRoomService _roomService;
        public UpdateRoomValidator(IRoomService roomService)
        {
            _roomService = roomService;

            RuleFor(x => x.Id)
                .Must(x => _roomService.GetByIdNoTracking(x) != null)
                .WithMessage("Phòng không tồn tại");

            RuleForEach(x => x.Slots)
                .ChildRules(slot =>
                {
                    slot.RuleFor(s => s.FromHour)
                        .NotEmpty()
                        .WithMessage("Giờ bắt đầu bắt buộc")
                        .Must(BeValidHour).WithMessage("Thời gian phải trong khoảng từ 0h đến 24h");

                    slot.RuleFor(s => s.ToHour)
                        .NotEmpty().WithMessage("Giờ kết thúc là bắt buộc")
                        .Must(BeValidHour).WithMessage("Thời gian phải trong khoảng từ 0h đến 24h")
                        .GreaterThan(x => x.FromHour).WithMessage("Giờ kết thúc phải lớn hơn giờ bắt đầu");
                });
        }

        private bool BeValidHour(string hour)
        {
            if (!int.TryParse(hour, out int hourValue))
            {
                return false;
            }
            return hourValue >= 0 && hourValue <= 24;
        }
    }
}