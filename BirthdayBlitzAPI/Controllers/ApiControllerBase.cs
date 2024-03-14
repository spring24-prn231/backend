using AutoMapper;
using BirthdayBlitzAPI.Filters;
using BusinessObjects.Common.Constants;
using BusinessObjects.Common.Enums;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BirthdayBlitzAPI.Controllers
{
    [ApiExceptionFilter]
    [ValidateModelState]
    [ApiController]
    [Route("api/v1/[controller]s/")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected  IActionResult GetResponse<TData>(AppResponse<TData> response) where TData : class
        {
            switch (response.Status)
            {
                case StatusResponse.Success:
                    return Ok(response);
                    break;
                case StatusResponse.Forbidden:
                    return Forbid();
                    break;
                case StatusResponse.Unauthorized:
                    response.Message = MessageResponse.Unauthorized;
                    return Unauthorized(response);
                    break;
                case StatusResponse.BadRequest:
                    return BadRequest(response);
                    break;
                case StatusResponse.NotFound:
                    response.Message = MessageResponse.NotFoundError;
                    return NotFound(response);
                    break;
                default:
                    break;
            }
            var details = new AppResponse<object>()
            {
                Status = StatusResponse.Fail,
                Message = MessageResponse.ServerError
            };
            return new ObjectResult(details)
            {
                StatusCode = 500
            };
        }
    }
}
