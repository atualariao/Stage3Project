using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using S3E1.Data;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace S3E1.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class UserAuth
    {
        private readonly RequestDelegate _next;

        public UserAuth(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string auth = httpContext.Request.Headers["x-user-id"];

            if(auth != null)
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync("Authentication Failed!");
                return;
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class UserAuthExtensions
    {
        public static IApplicationBuilder UseUserAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserAuth>();
        }
    }
}
