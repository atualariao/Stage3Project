using IntegrationTest.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace IntegrationTest.Handlers
{
    public class TestAuthHandler : AuthenticationHandler<TestAuthHandlerOptions>
    {
        public const string userId = "ED9C2025-018D-4135-BEC9-BC17AEA8AD47";
        public const string AutheticationScheme = "Test";
        private readonly string _authUser;
        private readonly Guid _authUserID;

        public TestAuthHandler(
            IOptionsMonitor<TestAuthHandlerOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) :
            base(options, logger, encoder, clock)
        {
            _authUser = options.CurrentValue.Username;
            _authUserID = options.CurrentValue.UserID;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, _authUser) };
            if (Context.Request.Headers.TryGetValue(_authUserID.ToString(), out var userId))
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userId[0]));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, _authUserID.ToString()));
            }

            var identity = new ClaimsIdentity(claims, AutheticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, AutheticationScheme);

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }
}
