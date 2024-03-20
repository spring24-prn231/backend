using BirthdayBlitzAPI.Attributes;
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
    public class VoucherController : ApiControllerBase
    {
        private readonly IVoucherService _voucherService;
        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetVoucherFilterRequest filter)
        {
            var response = await _voucherService.Get(filter).GetPaginatedResponse(page: filter.Page, pageSize: filter.PageSize);
            return Ok(response);
        }
        [Transaction]
        [HttpPost]
        [Authorize(Roles = "HOST_STAFF")]
        public async Task<IActionResult> Create([FromBody] CreateVoucherRequest request)
        {
            await _voucherService.Create(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.CreateSuccess
            });
        }
        [Transaction]
        [HttpPut]
        [Authorize(Roles = "HOST_STAFF")]
        public async Task<IActionResult> Update([FromBody] UpdateVoucherRequest request)
        {
            await _voucherService.Update(request);
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
            await _voucherService.Delete(id);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.DeleteSuccess
            });
        }
    }
}
