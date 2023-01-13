using S3E1.Data;

namespace S3E1.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class UserAuth
    {
        private readonly RequestDelegate _next;
        private readonly AppDataContext _appDataContextappDataContext;

        public UserAuth(RequestDelegate next, AppDataContext appDataContextappDataContext)
        {
            _next = next;
            _appDataContextappDataContext = appDataContextappDataContext;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var userList = _appDataContextappDataContext.Users.ToList();
            var user = userList.FirstOrDefault(x => x.UserID == new Guid("78cf4910-a00e-499f-a6ad-385bbcc5bbf7"));
            var UserID = user.UserID.ToString();

            if (userList.Count == 0)
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync("Authentication Failed!");
                return;
            }

            httpContext.TraceIdentifier = UserID;
            string id = httpContext.TraceIdentifier;
            httpContext.Response.Headers["x-user-id"] = id;

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
