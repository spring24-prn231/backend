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
    public class PartyPlanController : ApiControllerBase
    {
        private readonly IPartyPlanService _partyPlanService;
        private readonly IOrderService _orderService;
        private readonly IPartyPlanAssignmentService _partyPlanAssignmentService;
        public PartyPlanController(IPartyPlanService partyPlanService, IOrderService orderService, IPartyPlanAssignmentService partyPlanAssignmentService) 
        {
            _partyPlanAssignmentService = partyPlanAssignmentService;
            _orderService = orderService;
            _partyPlanService = partyPlanService;
        }
        [Authorize(Roles = "ADMIN, HOST_STAFF")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetPartyPlanFilterRequest filter)
        {
            var response = await _partyPlanService.Get(filter).GetPaginatedResponse(page: filter.Page, pageSize: filter.PageSize);
            return Ok(response);
        }
        [Transaction]
        [Authorize(Roles = "HOST_STAFF")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePartyPlanRequest request)
        {
            await _partyPlanService.Create(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.CreateSuccess
            });
        }
        [Transaction]
        [Authorize(Roles = "HOST_STAFF, ADMIN")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePartyPlanRequest request)
        {
            await _partyPlanService.Update(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.UpdateSuccess
            });
        }
        [Transaction]
        [Authorize(Roles = "HOST_STAFF, ADMIN")]
        [HttpPut("list")]
        public async Task<IActionResult> UpdatePartyPlan([FromBody] List<UpdatePartyPlanRequestList> request)
        {
            await _partyPlanService.UpdateList(request);

            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.UpdateSuccess
            });
        }
        [Transaction]
        [Authorize(Roles = "HOST_STAFF")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _partyPlanService.Delete(id);
            return Ok(new AppResponse<object>
                { 
                Message = MessageResponse.DeleteSuccess 
            });
        }
        [Transaction]
        [Authorize(Roles = "ADMIN")]
        [HttpPost("approvement")]
        public async Task<IActionResult> ApprovePlan([FromBody] ApprovePlanRequest request)
        {
            await _orderService.Approve(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.UpdateSuccess
            });
        }
        [Transaction]
        [Authorize(Roles = "ADMIN")]
        [HttpPost("staff-assigment")]
        public async Task<IActionResult> AssignStaff([FromBody] AssignStaffPlanRequest request)
        {
            await _partyPlanAssignmentService.Create(request);
            return Ok(new AppResponse<object>
            {
                Message = "Assign thành công"
            });
        }
    }
}