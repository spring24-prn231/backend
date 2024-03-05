using BusinessObjects.Common.Enums;
using BusinessObjects.Common.Extensions;
using BusinessObjects.Requests;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Controllers
{
    public class DishController : ApiControllerBase 
    {
        public readonly IDishService _dishService;
        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] GetDishFilterRequest filter)
        {
            var response = _dishService.Get(filter).GetPaginatedResponse(page: filter.Page, pageSize: filter.PageSize);
            return Ok(response);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateDishRequest request)
        {
            _dishService.Create(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.CreateSuccess
            });
        }
        [HttpPut]
        public IActionResult Update([FromBody] UpdateDishRequest request)
        {
            _dishService.Update(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.UpdateSuccess
            });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _dishService.Delete(id);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.DeleteSuccess
            });
        }
    }
}