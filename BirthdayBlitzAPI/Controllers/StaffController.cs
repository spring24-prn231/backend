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
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System.Security.Claims;

namespace BirthdayBlitzAPI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class StaffController : ApiControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticateService _authenticateService;
        public StaffController(UserManager<ApplicationUser> userManager, IAuthenticateService authenticateService)
        {
            _userManager = userManager;
            _authenticateService = authenticateService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetStaffFilterRequest filter)
        {
            var roles = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if (roles.Count == 1 && roles.Contains(UserRole.HOST_STAFF.ToString()))
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
            var allStaff = new List<ApplicationUser>();
            if(filter.GetHostStaff) allStaff.AddRange(await _userManager.GetUsersInRoleAsync(UserRole.HOST_STAFF.ToString()));
            if (filter.GetImplementStaff) allStaff.AddRange(await _userManager.GetUsersInRoleAsync(UserRole.IMPLEMENT_STAFF.ToString()));
            var queryRs = allStaff.Select(x => new { x.PhoneNumber, x.Fullname, x.UserName, x.Email, x.Id, x.Status });

            var response = await queryRs.GetPaginatedResponse(page: filter.Page, pageSize: filter.PageSize);
            return Ok(response);
        }

        [Transaction]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateStaffRequest request)
        {
            var rs = await _authenticateService.RegisterStaff(request);

            var response = new AppResponse<TokenModel>
            {
                Message = "Đăng ký thành công"
            };
            if (!rs.Succeeded)
            {
                response.Status = StatusResponse.BadRequest;
                response.Errors = rs.Errors.ToDictionary(x => x.Code, x => new string[] { x.Description });
                response.Message = "Đăng ký không thành công!";
            }
            return GetResponse(response);
        }

        [Transaction]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateStaffRequest request)
        {
            var response = new AppResponse<object>
            {
                Message = MessageResponse.UpdateSuccess
            };
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null || !(await _userManager.IsInRoleAsync(user, UserRole.IMPLEMENT_STAFF.ToString()) || await _userManager.IsInRoleAsync(user, UserRole.HOST_STAFF.ToString())))
            {
                throw new ValidationException();
            }
            if (request.Fullname != null)
            {
                user.Fullname = request.Fullname;
                await _userManager.UpdateAsync(user);
            }
            if (request.Password != null)
            {
                var pwToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                if (pwToken != null)
                {
                    var pwRs = await _userManager.ResetPasswordAsync(user, pwToken, request.Password);
                    if (!pwRs.Succeeded)
                    {
                        response.Status = StatusResponse.BadRequest;
                        response.Errors = pwRs.Errors.ToDictionary(x => x.Code, x => new string[] { x.Description });
                        response.Message = "Cập nhật không thành công!";
                        return GetResponse(response);
                    }
                }
            }
            if (request.Role != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.IsNullOrEmpty())
                {
                    await _userManager.RemoveFromRolesAsync(user, roles);
                }
                await _userManager.AddToRoleAsync(user, request.Role);
            }
            if (request.Email != null)
            {
                var emailToken = await _userManager.GenerateChangeEmailTokenAsync(user, request.Email);
                if (emailToken != null)
                {
                    var emailRs = await _userManager.ChangeEmailAsync(user, request.Email, emailToken);
                    if (!emailRs.Succeeded)
                    {
                        response.Status = StatusResponse.BadRequest;
                        response.Errors = emailRs.Errors.ToDictionary(x => x.Code, x => new string[] { x.Description });
                        response.Message = "Cập nhật không thành công!";
                        return GetResponse(response);
                    }
                }
            }
            if (request.PhoneNumber != null)
            {
                var phoneToken = await _userManager.GenerateChangePhoneNumberTokenAsync(user, request.PhoneNumber);
                if (phoneToken != null)
                {
                    var phoneRs = await _userManager.ChangePhoneNumberAsync(user, request.PhoneNumber, phoneToken);
                    if (!phoneRs.Succeeded)
                    {
                        response.Status = StatusResponse.BadRequest;
                        response.Errors = phoneRs.Errors.ToDictionary(x => x.Code, x => new string[] { x.Description });
                        response.Message = "Cập nhật không thành công!";
                        return GetResponse(response);
                    }
                }
            }
            return GetResponse(response);
        }

    }
}