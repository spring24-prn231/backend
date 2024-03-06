using BusinessObjects.Models;
using BusinessObjects.Requests;
using FluentValidation;
using Services.Implements;
using Services.Interfaces;
using System.Globalization;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateRoomValidator : AbstractValidator<UpdateRoomRequest>
    {
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;
        public UpdateRoomValidator(IRoomService roomService, IRoomTypeService roomTypeService)
        {
            _roomService = roomService;
            _roomTypeService = roomTypeService;
            RuleFor(x => x.RoomTypeId)
                .Must(x => _roomTypeService.GetByIdNoTracking(x) != null);

            RuleFor(x => x.RoomNo)
                .NotEmpty().WithMessage("Mã phòng không được để trống")
                .NotEqual(x => x.RoomNo).WithMessage("Mã phòng này đã tồn tại");
            RuleForEach(x => x.Slots)
                .ChildRules(slot =>
                {
                    slot.RuleFor(s => s.ToHour)
                        .Must((model, toHour) =>
                        {
                            return TryParseTime(toHour, out int parsedToHour, out int parsedToMinute);
                        })
                        .WithMessage("Giờ kết thúc không hợp lệ");

                    slot.RuleFor(s => s.FromHour)
                        .Must((model, fromHour) =>
                        {
                            return TryParseTime(fromHour, out int parsedFromHour, out int parsedToMinute);
                        }).WithMessage("Giờ bắt đầu không hợp lệ")
                        .Must((model, fromHour) =>
                        {
                            return DateTime.TryParseExact(fromHour, "H:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedFromHour) &&
                                   DateTime.TryParseExact(model.ToHour, "H:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedToHour) &&
                                   parsedFromHour < parsedToHour;
                        })
                        .WithMessage("Giờ bắt đầu phải nhỏ hơn giờ kết thúc");
                });                    
        }

        private static bool TryParseTime(string input, out int hour, out int minute)
        {
            hour = minute = 0;

            string[] parts = input.Split(':');
            if (parts.Length == 2 && int.TryParse(parts[0], out hour) && int.TryParse(parts[1], out minute))
            {
                if (hour >= 0 && hour < 24 && minute >= 0 && minute < 60)
                {
                    return true;
                }
            }
            return false;
        }    
    }
}