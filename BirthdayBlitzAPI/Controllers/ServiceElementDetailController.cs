using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Common.Enums;
using BusinessObjects.Common.Extensions;
using BusinessObjects.Requests;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Controllers
{
    public class ServiceElementDetailController : ApiControllerBase
    {
        private readonly IServiceElementDetailService _service;
        public ServiceElementDetailController(IServiceElementDetailService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] GetServiceElementDetailFilterRequest filter)
        {
            var response = _service.Get(filter).GetPaginatedResponse(page: filter.Page, pageSize: filter.PageSize);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateServiceElementDetailRequest request)
        {
            _service.Create(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.CreateSuccess
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.DeleteSuccess
            });
        }
    }
}