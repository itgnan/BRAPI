using System.Net;
using System.Runtime.CompilerServices;

using System.Text.Json;

namespace BREmployeeAPI
{
    public class BRExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<BRExceptionMiddleware> _logger;
        public BRExceptionMiddleware(RequestDelegate next, ILogger<BRExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception Occured");
                await HandleExceptionAsync(context, ex);
            }
        
        }

        public Task HandleExceptionAsync(HttpContext context, Exception ex) 
        {
            var code = HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = code,
                DevMessage = ex.Message,
                UserMessage = "Exception Occured ! Please contact Admin"
            };

            var payload = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(payload);

        }
    }
}
