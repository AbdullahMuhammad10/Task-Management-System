
using System.Text.Json;

namespace Backend.Helper
{
    // Middleware To Handle Exceptions Globally
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IWebHostEnvironment env;
        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger,IWebHostEnvironment env)
        {
            this.logger = logger;
            this.env = env;
        }
        public async Task InvokeAsync(HttpContext context,RequestDelegate next)
        {
            try
            {
                // To Call Next Middleware In The Pipeline If No Error Occurs
                await next.Invoke(context);
            }
            catch(Exception ex)
            {
                // Log Error Message To Console
                logger.LogError(ex.Message);

                // Return Custom Error Response
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                // Different Response Based On Environment 
                var response = env.IsDevelopment()
                    ? new ApiExceptionResponse(StatusCodes.Status500InternalServerError,ex.Message,ex.StackTrace!.ToString()) // Include Stack Trace In Development
                    : new ApiExceptionResponse(StatusCodes.Status500InternalServerError,ex.Message); // Generic Message In Production

                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }; // To Use Camel Case In JSON Response
                var jsonResponse = JsonSerializer.Serialize(response,options); // Serialize Response To JSON 
                await context.Response.WriteAsync(jsonResponse); // Write JSON Response To HTTP Response
            }
        }
    }
}
