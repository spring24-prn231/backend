using BusinessObjects.Models;
using BusinessObjects.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreateRoomValidator : AbstractValidator<CreateRoomRequest>
    {
        public readonly IRoomService _roomService;
        public CreateRoomValidator(IRoomService roomService)
        {
            _roomService = roomService;            

            RuleFor(x => x.Slots)
                .Must(slots => !SlotsContainDuplicates(slots))
                .WithMessage("Duplicate slots found.");
        }

        private bool SlotsContainDuplicates(ICollection<CreateSlotDto> slots)
        {
            var duplicateSlots = slots.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
            return duplicateSlots.Any();
        }     
    }
}
