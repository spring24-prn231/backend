using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayBlitzAPI.Attributes;
using BusinessObjects.Common.Enums;
using BusinessObjects.Common.Extensions;
using BusinessObjects.Requests;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Get([FromQuery] GetDepositFilterRequest filter)
        {
            var response = await _service.Get(filter).GetPaginatedResponse(page: filter.Page, pageSize: filter.PageSize);
            return Ok(response);
        }

        [Transaction]
        [HttpPost]
        [Authorize(Roles = "HOST_STAFF")]
        public async Task<IActionResult> Create([FromBody] CreateDepositRequest request)
        {
            await _service.Create(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.CreateSuccess
            });
        }

        [Transaction]
        [HttpPut]
        [Authorize(Roles = "HOST_STAFF")]
        public async Task<IActionResult> Update([FromBody] UpdateDepositRequest request)
        {
            await _service.Update(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.UpdateSuccess
            });
        }

        [Transaction]
        [HttpDelete("{id}")]
        [Authorize(Roles = "HOST_STAFF")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.DeleteSuccess
            });
        }
    }
}