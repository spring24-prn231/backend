using BusinessObjects.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreateSlotValidator : AbstractValidator<CreateSlotRequest>
    {
        public readonly ISlotService _slotService;
        public CreateSlotValidator(ISlotService slotService)
        {
            _slotService = slotService;
            RuleFor(x => x.ToHour)
                .NotEmpty().WithMessage("ToHour is required")
                .Must(BeValidHour).WithMessage("Hour must be in 0h to 24h")
                .GreaterThan(x => x.FromHour).WithMessage("ToHour must be greater than FromHour")
                .NotEqual(x => x.ToHour).WithMessage("This slot is available");

            RuleFor(x => x.FromHour)
                .NotEmpty().WithMessage("FromHour is required")
                .NotEqual(x => x.FromHour).WithMessage("This slot is available")
                .Must(BeValidHour).WithMessage("Hour must be in 0h to 24h");
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