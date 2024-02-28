using AutoMapper;
using BirthdayBlitzAPI.Filters;
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
    }
}
