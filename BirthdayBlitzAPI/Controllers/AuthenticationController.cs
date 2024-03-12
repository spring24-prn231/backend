using BusinessObjects.Common.Constants;
using BusinessObjects.Common.Enums;
using BusinessObjects.Requests;
using BusinessObjects.Requests.Authentication;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Controllers
{
    [Route("/api/v1/[controller]")]
    public class AuthenticationController : ApiControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        public AuthenticationController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var rs = await _authenticateService.Login(request);
            var response = new AppResponse<TokenModel>
            {
                Data = rs
            };
            if (rs != null)
            {
                response.Status = StatusResponse.Success;
                response.Message = MessageResponse.Authenticated;
            }
            else
            {
                response.Status = StatusResponse.Unauthorized;
            }
            return GetResponse(response);
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var rs = await _authenticateService.RegisterUser(request);

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
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel request)
        {
            var rs = await _authenticateService.RefreshToken(request);

            var response = new AppResponse<TokenModel>
            {
                Message = "Refresh thành công",
                Data = rs
            };
            if (rs is null) 
            { 
                response.Status = StatusResponse.BadRequest;
                response.Message = "AccessToken hoặc RefreshToken không hợp lệ!";
            }
            return GetResponse(response);
        }
    }
}
