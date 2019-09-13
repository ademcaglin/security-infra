using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Common.Tests.Setup
{
    public class OpenIdConnectStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication();

            var builder = services.AddIdentityServer(options =>
            {
                options.IssuerUri = "https://server";
            });

            builder.AddInMemoryClients(Clients.Get());
            builder.AddInMemoryApiResources(Scopes.GetApiScopes());

            builder.AddDeveloperSigningCredential(persistKey: false);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseIdentityServer();
            app.UseAuthentication();
        }
    }

    internal class Scopes
    {
        public static IEnumerable<ApiResource> GetApiScopes()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "api",
                    ApiSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    Scopes =
                    {
                        new Scope
                        {
                            Name = "api1"
                        }
                    }
                }
            };
        }
    }

    internal class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowOfflineAccess = true,
                    Claims =
                    {
                        new System.Security.Claims.Claim("application_name", "aaa")
                    },
                    AllowedScopes =
                    {
                        "api1"
                    }
                },
            };
        }
    }

}
