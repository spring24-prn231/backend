using BusinessObjects.Common.Constants;
using BusinessObjects.Common.Enums;
using BusinessObjects.Common.Extensions;
using BusinessObjects.Requests;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Implements;
using Services.Interfaces;
using System.Security.Claims;

namespace BirthdayBlitzAPI.Controllers
{
    public class OrderController : ApiControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetOrderFilterRequest filter)
        {
            var baseQuery = _orderService.Get(filter);
            var roleList = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).ToList();
            var _ = Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value, out Guid userId);
            if (roleList.Count == 1 && (roleList.FirstOrDefault().Value == UserRole.USER.ToString()))
            {
                baseQuery = baseQuery.Where(x => x.UserId == userId);
            }
            else if (roleList.Count == 1 && (roleList.FirstOrDefault().Value == UserRole.HOST_STAFF.ToString()))
            {
                baseQuery = baseQuery.Where(x => x.StaffId == userId);
            }
            var response = await baseQuery.GetPaginatedResponse(page: filter.Page, pageSize: filter.PageSize);
            return Ok(response);
        }
        [Authorize(Roles ="USER")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            var _ = Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value, out Guid userId);
            await _orderService.Create(request, userId);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.CreateSuccess
            });
        }
        [Authorize(Roles ="HOST_STAFF")]
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateOrderRequest request)
        {
            await _orderService.Update(request);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.UpdateSuccess
            });
        }
        [Authorize(Roles ="ADMIN")]
        [HttpPost("staff-assignment")]
        public async Task<IActionResult> Assign([FromBody] AssignStaffRequest request)
        {
            var staffUserName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            await _orderService.AssignStaff(request, staffUserName);
            return Ok(new AppResponse<object>
            {
                Message = "Assign thành công!"
            });
        }
        [Authorize(Roles ="HOST_STAFF")]
        [HttpPost("done")]
        public async Task<IActionResult> Done([FromBody] DoneOrderRequest request)
        {
            var _ = Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value, out Guid staffId);
            var rs = await _orderService.DoneOrder(request, staffId);
            var response = new AppResponse<object>
            {
                Status = rs ? StatusResponse.Success : StatusResponse.Forbidden,
                Message = "Assign thành công!"
            };

            return GetResponse(response);
        }
        [Authorize(Roles = "HOST_STAFF, ADMIN")]
        [HttpDelete("{id}")] 
        public async Task<IActionResult> Delete(Guid id)
        {
            await _orderService.Delete(id);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.DeleteSuccess
            });
        }
        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var response = await _orderService.GetAll().CountAsync();
            return Ok(new AppResponse<object>
            {
                Data = response
            }) ;
        }
    }
}
