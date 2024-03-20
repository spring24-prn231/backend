using BusinessObjects.Common.Constants;
using BusinessObjects.Common.Enums;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
namespace BirthdayBlitzAPI.Filters
{
    public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
    {
        public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
        {
            return new BadRequestObjectResult(new AppResponse<object>
            {
                Status = StatusResponse.BadRequest,
                Message = MessageResponse.BadRequestError,
                Errors = validationProblemDetails?.Errors
            });
        }
    }
}
