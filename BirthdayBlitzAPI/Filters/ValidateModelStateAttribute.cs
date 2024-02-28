using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects.Responses;
using BusinessObjects.Common.Constants;
using BusinessObjects.Common.Enums;
using System;

namespace BirthdayBlitzAPI.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToArray();
                var details = new AppResponse<object>
                {
                    Status = StatusResponse.BadRequest,
                    Message = MessageResponse.BadRequestError,
                    Errors = new Dictionary<string, string[]>
                    {
                        { "Invalid",errors}
                    }
                };

                context.Result = new BadRequestObjectResult(details);
            }
        }
    }
}
