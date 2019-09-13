using Microsoft.Extensions.DependencyInjection;
using SecurityInfra.Identity.Mongo.Repositories;
using System;

namespace SecurityInfra.Identity.Mongo
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIdsrvConfiguration(this IServiceCollection services, Action<ConfigurationDbOptions> optionsBuilder)
        {
            services.AddScoped<ConfigurationDbContext>();
            services.Configure<ConfigurationDbOptions>(optionsBuilder);
        }
    }
}
