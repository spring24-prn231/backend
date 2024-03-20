using AutoFilterer.Extensions;
using BirthdayBlitzAPI.Attributes;
using BusinessObjects.Common.Constants;
using BusinessObjects.Common.Enums;
using BusinessObjects.Common.Exceptions;
using BusinessObjects.Common.Extensions;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BirthdayBlitzAPI.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUserFilterRequest filter)
        {
            var roles = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList(); var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if (roles.Count == 1 && roles.Contains(UserRole.USER.ToString()))
            {
                var userResponse = new AppResponse<object>
                {
                    Status = StatusResponse.BadRequest,
                    Message = "Lấy thông tin không thành công"
                };
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null) return GetResponse(userResponse);
                var users = new List<ApplicationUser>
                {
                    user
                };
                userResponse.Status = StatusResponse.Success;
                userResponse.Data = users.Select(x => new { x.PhoneNumber, x.Fullname, x.UserName, x.Email, x.Id }).FirstOrDefault();
                return GetResponse(userResponse);
            }
            var queryRs = (await _userManager.GetUsersInRoleAsync(UserRole.USER.ToString())).Select(x => new { x.PhoneNumber, x.Fullname, x.UserName, x.Email, x.Id, x.Status });
            var response = await queryRs.GetPaginatedResponse(page: filter.Page, pageSize: filter.PageSize);
            return Ok(response);
        }
        [Transaction]
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null || !await _userManager.IsInRoleAsync(user, UserRole.USER.ToString()))
            {
                throw new ValidationException();
            }
            user.Status = false;
            await _userManager.UpdateAsync(user);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.DeleteSuccess
            });
        }
        [Transaction]
        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Activate(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null || !await _userManager.IsInRoleAsync(user, UserRole.USER.ToString()))
            {
                throw new ValidationException();
            }
            user.Status = true;
            await _userManager.UpdateAsync(user);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.UpdateSuccess
            });
        }
    }
}
