using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace jwttest.MyAuth
{
    public class MyAuthenticationHandler: AuthenticationHandler<MyAutenticationSchemeOptions>
    {
        public MyAuthenticationHandler(IOptionsMonitor<MyAutenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options,logger,encoder,clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var idClaim = Context.User.FindFirst("id");
            var id = idClaim.Value;
            var claimsPrincipal = TransClaimsPrincipal(id);
            var authenticationTicket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(authenticationTicket));
        }

        private ClaimsPrincipal TransClaimsPrincipal(string id)
        {
            var claims = new List<Claim>() {
                new Claim("id", id),
                new Claim("Name", "name222")
            };
            claims.AddRange(Enumerable.Range(1,10).Select(i => new Claim($"funcs",$"func{i}")));

            var claimsIdentity = new ClaimsIdentity(claims, Scheme.Name);
            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}