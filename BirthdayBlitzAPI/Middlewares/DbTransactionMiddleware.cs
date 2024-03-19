using BirthdayBlitzAPI.Attributes;
using Microsoft.AspNetCore.Http.Features;
using Services.Interfaces;
using System.Data;

namespace BirthdayBlitzAPI.Middlewares
{
    public class DbTransactionMiddleware
    {
        private readonly RequestDelegate _next;
        private ITransactionService _transactionService;

        public DbTransactionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ITransactionService transactionService)
        {
            _transactionService = transactionService;
            // For HTTP GET opening transaction is not required
            if (httpContext.Request.Method.Equals("GET", StringComparison.CurrentCultureIgnoreCase))
            {
                await _next(httpContext);
                return;
            }

            // If action is not decorated with TransactionAttribute then skip opening transaction
            var endpoint = httpContext.Features.Get<IEndpointFeature>()?.Endpoint;
            var attribute = endpoint?.Metadata.GetMetadata<TransactionAttribute>();
            if (attribute == null)
            {
                await _next(httpContext);
                return;
            }


            try
            {
                await _transactionService.CreateTransactionAsync();

                await _next(httpContext);
            }
            finally
            {
                if(transactionService.IsExist())
                await _transactionService.DisposeAsync();
            }
        }
    }
}
