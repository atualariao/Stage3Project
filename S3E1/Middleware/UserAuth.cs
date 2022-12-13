using Microsoft.IdentityModel.Tokens;
using S3E1.Data;

namespace S3E1.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class UserAuth
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UserAuth> _logger;
        private readonly AppDataContext appDataContextappDataContext;

        public UserAuth(RequestDelegate next, ILogger<UserAuth> logger, AppDataContext appDataContextappDataContext)
        {
            _next = next;
            _logger = logger;
            this.appDataContextappDataContext = appDataContextappDataContext;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var userList = appDataContextappDataContext.Users.ToList();
            if (userList.IsNullOrEmpty())
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync("Authentication Failed!");
                _logger.LogInformation("User doesn't exist");
                return;
            } 
            else
            {
                var user = userList.FirstOrDefault();
                var UserID = user.UserID.ToString();

                httpContext.TraceIdentifier = UserID;
                string id = httpContext.TraceIdentifier;
                httpContext.Response.Headers["x-user-id"] = id;

                _logger.LogInformation($"User Authentication: {id}.");
            }

            _logger.LogInformation("Authentication Complete.");
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
