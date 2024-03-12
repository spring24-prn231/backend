using AutoFilterer.Abstractions;
using BusinessObjects.Common.Extensions;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using FluentValidation;
using Services.Implements;
using Services.Interfaces;
using System.Globalization;
using System.Linq.Expressions;

namespace BirthdayBlitzAPI.Validators
{
    public class UpdateRoomValidator : AbstractValidator<UpdateRoomRequest>
    {
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;

        public UpdateRoomValidator(IRoomService roomService, IRoomTypeService roomTypeService)
        {
            UpdateRoomRequest filter = new UpdateRoomRequest();
            _roomService = roomService;
            _roomTypeService = roomTypeService;
            RuleFor(x => x.RoomTypeId)
                .Must(x => _roomTypeService.GetByIdNoTracking(x) != null);
            RuleFor(x => x.RoomNo)
                .Must(roomNo => !_roomService.GetAll().Any(room => room.RoomNo == roomNo))
                .WithMessage("Số phòng này đã tồn tại");                
        }    
    }
}