using eCommerceWebAPI.Data;
using Microsoft.IdentityModel.Tokens;

namespace eCommerceWebAPI.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class UserAuth
    {
        private readonly RequestDelegate _next;
        private readonly AppDataContext _context;

        public UserAuth(RequestDelegate next, AppDataContext context)
        {
            _next = next;
            _context = context;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.ContainsKey("x-user-id"))
            {
                httpContext.Response.StatusCode = 401;
                return;
            }

            string userId = httpContext.Request.Headers["x-user-id"];
            Guid parsedUserId = Guid.Parse(userId);

            if (!IsValidUserId(parsedUserId))
            {
                httpContext.Response.StatusCode = 401;
                return;
            }

            await _next(httpContext);
        }

        private bool IsValidUserId(Guid userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserID == userId);
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
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
