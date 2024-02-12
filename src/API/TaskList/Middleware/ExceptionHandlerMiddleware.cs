using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace TaskList.Middleware
{
    public sealed class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                HttpStatusCode code = ex switch
                {
                    KeyNotFoundException or FileNotFoundException => HttpStatusCode.NotFound,
                    UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                    ValidationException or InvalidOperationException => HttpStatusCode.BadRequest,
                    _ => HttpStatusCode.InternalServerError,
                };
                await HandleException(context, ex, code).ConfigureAwait(false);
            }
        }

        private static Task HandleException(HttpContext context, Exception exception, HttpStatusCode statusCodeInput)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)statusCodeInput;
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message + (exception.InnerException is not null ?
                    $" Inner exception: {exception.InnerException.Message}" : "")
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
