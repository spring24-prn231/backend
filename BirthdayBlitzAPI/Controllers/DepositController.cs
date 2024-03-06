using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Common.Enums;
using BusinessObjects.Common.Extensions;
using BusinessObjects.Requests;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Controllers
{
    public class DepositController : ApiControllerBase
    {
        private readonly IDepositService _service;
        public DepositController(IDepositService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] GetDepositFilterRequest filter)
        {
            var response = _service.Get(filter).GetPaginatedResponse(page: filter.Page, pageSize: filter.PageSize);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDepositRequest request)
        {
            _service.Create(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.CreateSuccess
            });
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateDepositRequest request)
        {
            _service.Update(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.UpdateSuccess
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