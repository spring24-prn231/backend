using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFilterer.Extensions;
using AutoMapper;
using BusinessObjects.Common.Constants;
using BusinessObjects.Common.Enums;
using BusinessObjects.Common.Extensions;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class StaffController : ApiControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticateService _authenticateService;
        private readonly IMapper _mapper;
        public StaffController(UserManager<ApplicationUser> userManager, IAuthenticateService authenticateService, IMapper mapper)
        {
            _userManager = userManager;
            _authenticateService = authenticateService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetStaffFilterRequest filter)
        {
            var roles = HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
            var _ = Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value, out Guid userId);
            var queryRs = _userManager.Users.ApplyFilter(filter).Select(x => new { x.PhoneNumber, x.Fullname, x.UserName, x.Email, x.Id });
            if (roles.Count == 3 && roles.Contains(UserRole.HOST_STAFF.ToString()) || roles.Count == 4 && roles.Contains(UserRole.IMPLEMENT_STAFF.ToString())) queryRs = queryRs.Where(x => x.Id == userId);
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
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            _mapper.Map(request, user);
            if (user == null || !(await _userManager.IsInRoleAsync(user, UserRole.IMPLEMENT_STAFF.ToString()) || await _userManager.IsInRoleAsync(user, UserRole.HOST_STAFF.ToString())))
            {
                throw new ValidationException();
            }
            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;
            await _userManager.UpdateAsync(user);
            return Ok(new AppResponse<object>
            {
                Message = MessageResponse.UpdateSuccess
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Activate(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null || !(await _userManager.IsInRoleAsync(user, UserRole.IMPLEMENT_STAFF.ToString()) || await _userManager.IsInRoleAsync(user, UserRole.HOST_STAFF.ToString())))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null || !(await _userManager.IsInRoleAsync(user, UserRole.IMPLEMENT_STAFF.ToString()) || await _userManager.IsInRoleAsync(user, UserRole.HOST_STAFF.ToString())))
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
    }
}