using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarModelService.BusinessLayer.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarModelService.Utility
{
    public class ApiExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionHandlingMiddleware> _logger;

        public ApiExceptionHandlingMiddleware(RequestDelegate next, ILogger<ApiExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            string result;

            if (ex is SystemBaseException)
            {
                SystemBaseException exception = (SystemBaseException)ex;
                var errorTitle = "System error.";
                switch (exception.errorCode)
                {
                    case SystemErrorCode.ValidationError:
                        errorTitle = "Validation errors occurred.";
                        break;
                    case SystemErrorCode.EntityNotFound:
                        errorTitle = "Requested entity was not found.";
                        break;
                    case SystemErrorCode.CreditsMissing:
                        errorTitle = "Credits missing unable to perform operation.";
                        break;
                    default:
                        break;
                }
                Console.WriteLine("error message: "+ex.Message);
                var problemDetails = new ValidationProblemDetails(new Dictionary<string, string[]> { 
                    { 
                        "Error", new[] { exception.Message }
                    },
                    { 
                        "Inner Error", new[]{ exception.InnerException?.Message }
                    }
                })
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = errorTitle,
                    Status = (int)HttpStatusCode.BadRequest,
                    Instance = context.Request.Path,
                };
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(problemDetails);
            }
            else
            {
                Console.WriteLine("error message: " + ex.Message);
                _logger.LogError(ex, $"An unhandled exception has occurred, {ex.Message}");
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = "Internal Server Error.",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Instance = context.Request.Path,
                    Detail = "Internal Server Error!"
                };
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result = JsonSerializer.Serialize(problemDetails);
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }
    }
}
