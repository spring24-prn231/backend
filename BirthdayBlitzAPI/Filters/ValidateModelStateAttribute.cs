using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects.Responses;
using BusinessObjects.Common.Constants;
using BusinessObjects.Common.Enums;
using System;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        private ITransactionService _transactionService;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _transactionService = context.HttpContext.RequestServices.GetRequiredService<ITransactionService>();
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

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _transactionService = context.HttpContext.RequestServices.GetRequiredService<ITransactionService>();
            if (context.Exception != null)
            {
                if (_transactionService.IsExist())
                {
                    var rollback = _transactionService.RollbackAsync();
                    rollback.Wait();
                }
            }
            else
            {
                var commit = _transactionService.CommitAsync();
                commit.Wait();
            }
            base.OnActionExecuted(context);
        }
    }
}
