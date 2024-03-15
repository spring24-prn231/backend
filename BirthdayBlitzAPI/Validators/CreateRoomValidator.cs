using BusinessObjects.Requests;
using FluentValidation;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreateRoomValidator : AbstractValidator<CreateRoomRequest>
    {
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;
        public CreateRoomValidator(IRoomService roomService, IRoomTypeService roomTypeService)
        {
            _roomService = roomService;
            _roomTypeService = roomTypeService;
            RuleFor(x => x.RoomTypeId)
                .Must(x => _roomTypeService.GetByIdNoTracking(x) != null)
                .WithMessage("Loại phòng này tìm không thấy");
            RuleFor(x => x.RoomNo)
                .Must(roomNo => !_roomService.GetAll().Any(r => r.RoomNo == roomNo))
                .WithMessage("Số phòng này đã tồn tại");
        }
    }
}
