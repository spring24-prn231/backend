using BusinessObjects.Common.Enums;
using BusinessObjects.Common.Extensions;
using BusinessObjects.Requests;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Controllers
{
    public class PartyPlanController : ApiControllerBase
    {
        private readonly IPartyPlanService _partyPlanService;
        public PartyPlanController(IPartyPlanService partyPlanService) 
        {
            _partyPlanService = partyPlanService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetPartyPlanFilterRequest filter)
        {
            var response = await _partyPlanService.Get(filter).GetPaginatedResponse(page: filter.Page, pageSize: filter.PageSize);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePartyPlanRequest request)
        {
            await _partyPlanService.Create(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.CreateSuccess
            });
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePartyPlanRequest request)
        {
            await _partyPlanService.Update(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.UpdateSuccess
            });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _partyPlanService.Delete(id);
            return Ok(new AppResponse<object>
                { 
                Message = MessageResponse.DeleteSuccess 
            });
        }
    }
}