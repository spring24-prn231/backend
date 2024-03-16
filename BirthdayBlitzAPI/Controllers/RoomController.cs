using BusinessObjects.Common.Enums;
using BusinessObjects.Common.Extensions;
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
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetRoomFilterRequest filter)
        {
            var response = await _roomService.Get(filter).GetPaginatedResponse(page: filter.Page, filter.PageSize);
            return Ok(response);
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] CreateRoomRequest request)
        {
            await _roomService.Create(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.CreateSuccess
            });
        }
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update([FromBody] UpdateRoomRequest request)
        {
            await _roomService.Update(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.UpdateSuccess
            });
        }
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
