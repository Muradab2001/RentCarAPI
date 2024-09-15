using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RentCarApi.Domain.Rules;
using System.Net;
using System.Text.Json;

namespace RentCarApi.Application.Middleware
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Handled exception");
                var problem = CreateProblemDetails(context, ex);
                context.Response.StatusCode = problem.Status.Value;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
            }
        }

        private static ProblemDetails CreateProblemDetails(HttpContext context, Exception ex)
        {
            if (IsDuplicateKeyViolation(ex))
            {
                return new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = "Duplicate Key Error ",
                    Title = "Duplicate Key Violation",
                    Detail = "The item already exists."
                };
            }
            else if (ex is INonSensitiveException)
            {
                return new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server Error",
                    Title = ex.GetType().Name,
                    Detail = ex.Message
                };
            }
            return new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server Error",
                Title = "An internal error has occurred",
                Detail = "An internal server error has occurred. Please try again later."
            };
        }

        private static bool IsDuplicateKeyViolation(Exception e)
        {
            return e.InnerException?.Message.Contains("Duplicate entry") ?? false;
        }
    }
}