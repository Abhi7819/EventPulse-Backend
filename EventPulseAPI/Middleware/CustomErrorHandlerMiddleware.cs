using System.Net;
using System.Text.Json;

namespace EventPulseAPI.Middleware
{
    public class CustomErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    await WriteResponse(context, 401, "Unauthorized: Token missing, invalid, or expired.");
                }
                else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    await WriteResponse(context, 403, "Forbidden: You do not have permission to access this resource.");
                }
                else if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    await WriteResponse(context, 404, "Resource not found.");
                }
                else if (context.Response.StatusCode == (int)HttpStatusCode.MethodNotAllowed)
                {
                    await WriteResponse(context, 405, "HTTP Method not allowed.");
                }
                else if (context.Response.StatusCode == (int)HttpStatusCode.BadRequest)
                {
                    await WriteResponse(context, 400, "Bad Request: Invalid input.");
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task WriteResponse(HttpContext context, int status, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;

            var result = JsonSerializer.Serialize(new
            {
                status = status,
                message = message
            });

            await context.Response.WriteAsync(result);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(new
            {
                status = 500,
                message = "An unexpected error occurred on the server.",
                detail = ex.Message
            });

            return context.Response.WriteAsync(result);
        }
    }
}
