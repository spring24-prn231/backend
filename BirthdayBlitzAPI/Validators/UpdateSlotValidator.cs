using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;
using System.Globalization;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateSlotValidator : AbstractValidator<UpdateSlotRequest>
    {
        private readonly ISlotService _slotService;
        public UpdateSlotValidator(ISlotService slotService)
        {
            _slotService = slotService;
            //Validation rule for To Hour
            RuleFor(x => x.ToHour)
                .Must((model, toHour) =>
                {
                    return TryParseTime(toHour, out int parsedToHour, out int parsedToMinute);
                })
                .WithMessage("Giờ kết thúc không hợp lệ");

            //Validation rule for FromHour
            RuleFor(x => x.FromHour)
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