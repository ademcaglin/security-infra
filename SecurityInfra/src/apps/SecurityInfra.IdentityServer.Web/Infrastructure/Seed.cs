using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SecurityInfra.Configuration.ApiResources;
using SecurityInfra.Configuration.Clients;
using SecurityInfra.Configuration.IdentityResources;
using SecurityInfra.Configuration.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.IdentityServer.Web.Infrastructure
{
    public class Seed
    {
        public static void Create(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var env = services.GetRequiredService<IHostingEnvironment>();
                    if (env.IsDevelopment())
                    {
                        AddClients(services);
                        AddIdentityResources(services);
                        AddApiResources(services);
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        private static void AddClients(IServiceProvider services)
        {
            var rep = services.GetRequiredService<IClientRepository>();

            rep.Save(new Configuration.Clients.Client()
            {
                Id = "implicit",
                TenantId = "sample",
                RedirectUris = new List<string>() { "http://localhost:5005/signin-oidc" },
                PostLogoutRedirectUris = new List<string>() { "http://localhost:5005/signout-callback-oidc" },
                ClientName = "Implicit Sample",
                ClientId = "implicit",
                AllowedGrantTypes = new List<string>() { "implicit" },
                AllowedScopes = new List<string>() { "openid", "profile" }
            });

            rep.Save(new Configuration.Clients.Client()
            {
                Id = "sample",
                TenantId = "sample",
                RedirectUris = new List<string>() { "http://localhost:5005/signin-oidc" },
                PostLogoutRedirectUris = new List<string>() { "http://localhost:5005/signout-callback-oidc" },
                ClientName = "Sample",
                ClientId = "sample",
                AllowedGrantTypes = new List<string>() { "hybrid" },
                AllowedScopes = new List<string>() { "openid", "profile" },
                ClientSecrets = new List<Configuration.Secret>()
                {
                    new Configuration.Secret()
                    {
                          Type = IdentityServerConstants.SecretTypes.SharedSecret,
                          Value = "codesample_pwd".Sha256()
                    }
                }
            });

            rep.Save(new Configuration.Clients.Client()
            {
                Id = "codesample",
                TenantId = "sample",
                RedirectUris = new List<string>() { "http://localhost:5005/signin-oidc" },
                PostLogoutRedirectUris = new List<string>() { "http://localhost:5005/signout-callback-oidc" },
                ClientName = "Code Sample",
                ClientId = "codesample",
                AllowedGrantTypes = new List<string>() { "authorization_code" },
                AllowedScopes = new List<string>() { "openid", "profile" },
                ClientSecrets = new List<Configuration.Secret>()
                {
                    new Configuration.Secret()
                    {
                       Type = IdentityServerConstants.SecretTypes.SharedSecret,
                       Value = "sample_pwd".Sha256()
                    }
                }
            });

            rep.Save(new Configuration.Clients.Client()
            {
                Id = "client_credentials",
                TenantId = "sample",
                RedirectUris = new List<string>() { "http://localhost:5005/signin-oidc" },
                PostLogoutRedirectUris = new List<string>() { "http://localhost:5005/signout-callback-oidc" },
                ClientName = "Client Credentials Sample",
                ClientId = "client_credentials",
                AllowedGrantTypes = new List<string>() { "client_credentials" },
                ClientSecrets = new List<Configuration.Secret>()
                {
                    new Configuration.Secret()
                    {
                        Type = IdentityServerConstants.SecretTypes.SharedSecret,
                        Value = "client_credentials_pwd".Sha256()
                    }
                },
                AllowedScopes = new List<string>() { "api1" },
            });
        }

        private static void AddIdentityResources(IServiceProvider services)
        {
            var idrep = services.GetRequiredService<IIdentityResourceRepository>();
            idrep.Save(new Configuration.IdentityResources.IdentityResource()
            {
                Id = "openid",
                Name = "openid"
            });
            idrep.Save(new Configuration.IdentityResources.IdentityResource()
            {
                Id = "profile",
                Name = "profile"
            });
            idrep.Save(new Configuration.IdentityResources.IdentityResource()
            {
                Id = "personal_info",
                Name = "personal information",
                UserClaims = new List<string>() { "", "", "" }
            });
            idrep.Save(new Configuration.IdentityResources.IdentityResource()
            {
                Id = "communication_info",
                Name = "communication information",
                UserClaims = new List<string>() { "", "", "" }
            });
            idrep.Save(new Configuration.IdentityResources.IdentityResource()
            {
                Id = "address",
                Name = "address",
                UserClaims = new List<string>() { "", "", "" }
            });
            idrep.Save(new Configuration.IdentityResources.IdentityResource()
            {
                Id = "personal_info",
                Name = "personal information"
            });
        }

        private static void AddApiResources(IServiceProvider services)
        {
            var apirep = services.GetRequiredService<IApiResourceRepository>();
            apirep.Save(new Configuration.ApiResources.ApiResource()
            {
                Id = "api1",
                Name = "api1",
                DisplayName = "api1",
                Description = "api1",
                Scopes = new List<Configuration.Scope>() { new Configuration.Scope() { Name = "api1" } }
            });
        }

        private static void AddTenants(IServiceProvider services)
        {
            var rep = services.GetRequiredService<ITenentRepository>();
            rep.Save(new Tenant()
            {
                Id = "sys_admin",
                Title = "Sistem Yönetimi"
            });

            rep.Save(new Tenant()
            {
                Id = "dbys",
                Title = "Denizci Bilgi Yönetim Sistemi"
            });
        }

        private static void AddMenuProviders(IServiceProvider services)
        {
            var rep = services.GetRequiredService<ITenentRepository>();
        }

        private static void AddIdentityRoles(IServiceProvider services)
        {
            var rep = services.GetRequiredService<ITenentRepository>();
        }

        private static void AddIdentityRoleResources(IServiceProvider services)
        {
            var rep = services.GetRequiredService<ITenentRepository>();
        }

        private static void AddIdentityUsers(IServiceProvider services)
        {
            var rep = services.GetRequiredService<ITenentRepository>();
        }
    }
}
