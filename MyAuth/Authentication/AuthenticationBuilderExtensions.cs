using System;
using Microsoft.AspNetCore.Authentication;

namespace jwttest.MyAuth
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddMyAuthentication(this AuthenticationBuilder builder, Action<MyAutenticationSchemeOptions> configureOptions = null)
        {
            return builder.AddScheme<MyAutenticationSchemeOptions, MyAuthenticationHandler>(MyAuthenticationDefaults.SchemeName, configureOptions);
        }
    }
}