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
        public readonly IDishTypeService _dishTypeService;
        public DishTypeController(IDishTypeService dishTypeService)
        {
            _dishTypeService = dishTypeService;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] GetDishTypeFilterRequest filter)
        {
            var response = _dishTypeService.Get(filter).GetPaginatedResponse(page: filter.Page, pageSize: filter.PageSize);
            return Ok(response);
        }
        [HttpPost]
        public IActionResult Create([FromBody]CreateDishTypeRequest request)
        {
            _dishTypeService.Create(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.CreateSuccess
            });
        }
        [HttpPut]
        public IActionResult Update([FromBody]UpdateDishTypeRequest request)
        {
            _dishTypeService.Update(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.UpdateSuccess
            });
        }
        [HttpDelete("{id}")]    
        public IActionResult Delete(Guid id)
        {
            _dishTypeService.Delete(id);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.DeleteSuccess
            });
        }
    }
}
