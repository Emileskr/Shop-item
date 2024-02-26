using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ShopItemAPI.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<Middleware> _logger;

        public Middleware(RequestDelegate next, ILogger<Middleware>logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            int statusCode = 200;
            var a = httpContext.Request.Headers["Auth"];

            if(a == "myKey")
            {
                await _next(httpContext);
                return;
            } 
            else
            {
                httpContext.Response.StatusCode = 401;
                return;
            }
           
            httpContext.Response.StatusCode = statusCode;
            await HandleExceptionsAsync($"Error occured", httpContext);
        }
        private Task HandleExceptionsAsync(string exception, HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            _logger.LogError(exception);
            return httpContext.Response.WriteAsync(new ErrorModel
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = exception
            }.ToString());

        } 
        
    }
   

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }
    }
}
