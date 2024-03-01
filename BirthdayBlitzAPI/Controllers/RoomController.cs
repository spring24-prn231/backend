using BusinessObjects.Common.Enums;
using BusinessObjects.Common.Extensions;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Controllers
{
    public class RoomController : ApiControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] GetRoomFilterRequest filter)
        {            
                var response = _roomService.Get(filter).GetPaginatedResponse(page: filter.Page, pageSize: filter.PageSize);
                return Ok(response);   
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateRoomRequest request)
        {
            _roomService.Create(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.CreateSuccess
            });
        }
        [HttpPut]
        public IActionResult Update([FromBody] UpdateRoomRequest request)
        {
            _roomService.Update(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.UpdateSuccess
            });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _roomService.Delete(id);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.DeleteSuccess
            });
        }
    }
}
