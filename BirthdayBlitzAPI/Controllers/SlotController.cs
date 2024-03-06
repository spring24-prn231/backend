using BirthdayBlitzAPI.Validators;
using BusinessObjects.Common.Enums;
using BusinessObjects.Common.Extensions;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Controllers
{
    public class SlotController : ApiControllerBase
    {
<<<<<<< HEAD
        private ISlotService _slotService;
=======
        public ISlotService _slotService;
>>>>>>> 008d452530facb7529bca1376860b3a17d14050d
        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] GetSlotFilterRequest filter)
        {
            var response = _slotService.Get(filter).GetPaginatedResponse(page: filter.Page, pageSize: filter.PageSize);
            return Ok(response);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateSlotRequest request)
        {
            _slotService.Create(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.CreateSuccess
            });
        }
        [HttpPut]
        public IActionResult Update([FromBody] UpdateSlotRequest request)
        {           
            _slotService.Update(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.UpdateSuccess
            });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _slotService.Delete(id);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.DeleteSuccess
            });
        }
    }
}