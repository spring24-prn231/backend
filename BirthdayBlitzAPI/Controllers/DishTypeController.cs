using BusinessObjects.Common.Enums;
using BusinessObjects.Common.Extensions;
using BusinessObjects.Requests;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Controllers
{
    public class DishTypeController : ApiControllerBase
    {
        private readonly IDishTypeService _dishTypeService;
        public DishTypeController(IDishTypeService dishTypeService)
        {
            _dishTypeService = dishTypeService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetDishTypeFilterRequest filter)
        {
            var response = await _dishTypeService.Get(filter).GetPaginatedResponse(page: filter.Page, pageSize: filter.PageSize);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateDishTypeRequest request)
        {
            await _dishTypeService.Create(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.CreateSuccess
            });
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateDishTypeRequest request)
        {
            await _dishTypeService.Update(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.UpdateSuccess
            });
        }
        [HttpDelete("{id}")]    
        public async Task<IActionResult> Delete(Guid id)
        {
            await _dishTypeService.Delete(id);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.DeleteSuccess
            });
        }
    }
}