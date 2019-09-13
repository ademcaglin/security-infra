using Microsoft.Extensions.DependencyInjection;
using SecurityInfra.Configuration.ApiResources;
using SecurityInfra.Configuration.IdentityResources;
using SecurityInfra.Configuration.MenuProviders;
using SecurityInfra.Configuration.Clients;
using SecurityInfra.Configuration.Mongo.Repositories;
using Microsoft.Extensions.Options;
using System;

namespace SecurityInfra.Configuration.Mongo
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIdsrvConfiguration(this IServiceCollection services, Action<ConfigurationDbOptions> optionsBuilder)
        {
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IApiResourceRepository, ApiResourceRepository>();
            services.AddScoped<IIdentityResourceRepository, IdentityResourceRepository>();
            services.AddScoped<ConfigurationDbContext>();
            services.Configure<ConfigurationDbOptions>(optionsBuilder);
        }
    }
}
