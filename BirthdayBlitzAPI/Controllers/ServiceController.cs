using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BirthdayBlitzAPI.Attributes;
using BusinessObjects.Common.Enums;
using BusinessObjects.Common.Extensions;
using BusinessObjects.Requests;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Controllers
{
    public class ServiceController : ApiControllerBase
    {
        private readonly IServiceService _service;
        public ServiceController(IServiceService service)
        {
            _service = service;
        }
        [Authorize(Roles ="ADMIN, HOST_STAFF")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetServiceFilterRequest filter)
        {
            var response = await _service.Get(filter).GetPaginatedResponse(page: filter.Page, pageSize: filter.PageSize);
            return Ok(response);
        }
        [HttpGet("anonymous")]
        public async Task<IActionResult> GetAnonymous([FromQuery] GetServiceFilterRequest filter)
        {
            var response = await _service.Get(filter).Where(x=>x.UserId == null).GetPaginatedResponse(page: filter.Page, pageSize: filter.PageSize);
            return Ok(response);
        }

        [Transaction]
        [HttpPost]
        [Authorize(Roles = "HOST_STAFF")]
        public async Task<IActionResult> Create([FromBody] CreateServiceRequest request)
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
        public async Task<IActionResult> Update([FromBody] UpdateServiceRequest request)
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