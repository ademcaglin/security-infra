using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace FakeAuthentication
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseFakeAuthentication(this IApplicationBuilder app)
        {
            app.UseMiddleware<FakeAuthenticationMiddleware>();
        }
    }
}
