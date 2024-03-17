using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFilterer.Extensions;
using AutoMapper;
using Azure;
using BusinessObjects.Common.Constants;
using BusinessObjects.Common.Enums;
using BusinessObjects.Common.Extensions;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;

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
            var _ = Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value, out Guid userId);
            var queryRs = _userManager.Users.ApplyFilter(filter).Select(x => new { x.PhoneNumber, x.Fullname, x.UserName, x.Email, x.Id, x.Status });
            if (roles.Count == 1 && roles.Contains(UserRole.HOST_STAFF.ToString())) queryRs = queryRs.Where(x => x.Id == userId && x.Status);
            var response = await queryRs.GetPaginatedResponse(page: filter.Page, pageSize: filter.PageSize);
            return Ok(response);
        }

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
                await _userManager.AddToRoleAsync(user,request.Role);
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