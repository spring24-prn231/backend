using BirthdayBlitzAPI.Attributes;
using BusinessObjects.Common.Enums;
using BusinessObjects.Common.Extensions;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Implements;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Controllers
{
    public class RoomController : ApiControllerBase
    {
        private IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        [Authorize(Roles = "ADMIN, HOST_STAFF")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetRoomFilterRequest filter)
        {
            var response = await _roomService.Get(filter).GetPaginatedResponse(page: filter.Page, filter.PageSize);
            return Ok(response);
        }
        [HttpGet("anonymous")]
        public async Task<IActionResult> GetByAnonymous([FromQuery] GetRoomFilterRequest filter)
        {
            filter.IsEager = true;
            var allRooms = _roomService.Get(filter).ToList();
            var response = new AppResponse<IEnumerable<Room>>();
            if (allRooms.Any())
            {
                response.Data = allRooms.GroupBy(x => x.RoomTypeId).Select(y =>
               {
                   var room = y.FirstOrDefault();
                   room.Capacity = y.Max(x => x.Capacity);
                   return new Room
                   {
                       Id = room.Id,
                       RoomTypeId = room.RoomTypeId,
                       Capacity = room.Capacity,
                       Name = room.RoomType.Name,
                       Description = room.RoomType.Description,
                       Image = room.Image,
                       Price = room.Price,
                       Status = room.Status,
                       RoomType = room.RoomType,
                   };
                }).ToList();
            }
            response = await response.Data.GetPaginatedResponse(page: filter.Page, filter.PageSize);
            return Ok(response);
        }
        [Transaction]
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromForm] CreateRoomRequest request)
        {
            await _roomService.Create(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.CreateSuccess
            });
        }
        [Transaction]
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update([FromForm] UpdateRoomRequest request)
        {
            await _roomService.Update(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.UpdateSuccess
            });
        }
        [Transaction]
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _roomService.Delete(id);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.DeleteSuccess
            });
        }
    }
}
