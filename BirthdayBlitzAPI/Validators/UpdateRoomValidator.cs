using BusinessObjects.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

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
                .MustAsync(async (x, cancellationToken) => await _roomTypeService.GetByIdNoTracking(x) != null)
                .WithMessage("Loại phòng này tìm không thấy");
            RuleFor(x => x.RoomNo)
                .MustAsync(async (roomNo, cancellationToken) => !await _roomService.GetAll().AnyAsync(r => r.RoomNo == roomNo, cancellationToken))
                .WithMessage("Số phòng này đã tồn tại");
        }
    }
}
