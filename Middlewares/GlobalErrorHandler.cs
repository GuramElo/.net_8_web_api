using System.Net;

namespace Reddit.Middlewares
{
    //davamate es
    public class GlobalErrorHandler(RequestDelegate next, ILogger<GlobalErrorHandler> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<GlobalErrorHandler> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            switch (exception)
            {
                /*
                 *  case ValidationException validationException:
                     context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                     await context.Response.WriteAsJsonAsync(new { StatusCode = context.Response.StatusCode, Errors = validationException.Errors });
                     _logger.LogError(validationException.Message);
                     break;

                 case ProductNotFoundException notFoundException:
                     context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                     await context.Response.WriteAsJsonAsync(new { StatusCode = context.Response.StatusCode, Errors = notFoundException.Message });
                     _logger.LogError(notFoundException.Message);
                     break;
                 */
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsJsonAsync(new { StatusCode = context.Response.StatusCode, Message = "Unexpected error occurred on the server" });
                    _logger.LogError(exception.Message);
                    break;
            }
        }
    }
}
