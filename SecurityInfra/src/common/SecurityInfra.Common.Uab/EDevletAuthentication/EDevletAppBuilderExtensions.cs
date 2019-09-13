using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.EDevlet;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EDevletAuthenticationOptionsExtensions
    {
        public static AuthenticationBuilder AddEDevlet(this AuthenticationBuilder builder)
            => builder.AddEDevlet(EDevletDefaults.AuthenticationScheme, _ => { });

        public static AuthenticationBuilder AddEDevlet(this AuthenticationBuilder builder, Action<EDevletOptions> configureOptions)
            => builder.AddEDevlet(EDevletDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddEDevlet(this AuthenticationBuilder builder, string authenticationScheme, Action<EDevletOptions> configureOptions)
            => builder.AddEDevlet(authenticationScheme, EDevletDefaults.DisplayName, configureOptions);

        public static AuthenticationBuilder AddEDevlet(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<EDevletOptions> configureOptions)
            => builder.AddOAuth<EDevletOptions, EDevletHandler>(authenticationScheme, displayName, configureOptions);
    }
}