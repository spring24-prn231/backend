using AutoFilterer.Extensions;
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
            var roles = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
            var _ = Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value, out Guid userId);
            var queryRs = _userManager.Users.ApplyFilter(filter).Select(x => new { x.PhoneNumber, x.Fullname, x.UserName, x.Email, x.Id , x.Status});
            if (roles.Count == 1 && roles.Contains(UserRole.USER.ToString())) queryRs = queryRs.Where(x => x.Id == userId);
            var response = await queryRs.GetPaginatedResponse(page: filter.Page, pageSize: filter.PageSize);
            return Ok(response);
        }
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
        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Acitvate(Guid id)
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
