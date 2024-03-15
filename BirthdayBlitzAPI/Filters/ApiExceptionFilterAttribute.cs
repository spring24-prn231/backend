using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects.Responses;
using BusinessObjects.Common;
using System;
using BusinessObjects.Common.Constants;
using BusinessObjects.Common.Enums;
using BusinessObjects.Common.Exceptions;
using System.Diagnostics;

namespace BirthdayBlitzAPI.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        public ApiExceptionFilterAttribute()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(ConflictException), HandleConflictException }
            };
        }

        private void HandleConflictException(ExceptionContext context)
        {
            var exception = (ConflictException)context.Exception;

            var details = new AppResponse<object>()
            {
                Status = StatusResponse.Fail,
                Message = MessageResponse.ConflictError
            };

            context.Result = new ConflictObjectResult(details);

            context.ExceptionHandled = true;
        }
        private void HandleUnknownException(ExceptionContext context)
        {
#if DEBUG
            Debug.WriteLine(context.Exception.Message);
#endif
            var details = new AppResponse<object>()
            {
                Status = StatusResponse.Fail,
                Message = MessageResponse.ServerError 
            };
            context.Result = new ObjectResult(details)
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }
            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }
            HandleUnknownException(context);
            return;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = (ValidationException)context.Exception;

            var details = new AppResponse<object>
            {
                Status = StatusResponse.BadRequest,
                Message = MessageResponse.BadRequestError,
                Errors = exception.Errors
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new AppResponse<object>()
            {
                Status = StatusResponse.BadRequest,
                Message = MessageResponse.BadRequestError
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = (NotFoundException)context.Exception;

            var details = new AppResponse<object>()
            {
                Status = StatusResponse.NotFound,
                Message = MessageResponse.NotFoundError
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}
