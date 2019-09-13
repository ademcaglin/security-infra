using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FakeAuthentication
{
    public class FakeAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public FakeAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context,
            IAuthenticationSchemeProvider schemeProvider,
            IOptionsMonitorCache<FakeAuthenticationSchemeOptions> optionsCache)
        {
            var fakeOptions = context.RequestServices
                      .GetRequiredService<IOptionsSnapshot<FakeAuthenticationOptions>>().Value;
            foreach (var fakeScheme in fakeOptions.Schemes)
            {
                if (await schemeProvider.GetSchemeAsync(fakeScheme.SchemeName) != null)
                {
                    schemeProvider.RemoveScheme(fakeScheme.SchemeName);
                    optionsCache.TryRemove(fakeScheme.SchemeName);
                    var scheme = new AuthenticationScheme(fakeScheme.SchemeName,
                        fakeScheme.SchemeName,
                        typeof(FakeAuthenticationHandler));
                    schemeProvider.AddScheme(scheme);
                    var fakeSchemeOptions = new FakeAuthenticationSchemeOptions();
                    foreach (var claim in fakeScheme.Claims)
                        fakeSchemeOptions.Claims.Add(claim);
                    optionsCache.TryAdd(fakeScheme.SchemeName, fakeSchemeOptions);
                }
            }
            await _next(context);
        }
    }
}
