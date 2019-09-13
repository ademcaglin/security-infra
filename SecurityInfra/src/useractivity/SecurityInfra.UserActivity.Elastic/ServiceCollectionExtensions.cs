using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace SecurityInfra.UserActivity.Elastic
{
    public static class ServiceCollectionExtensions
    {
        public static void AddUserActivity(this IServiceCollection services, Action<ElasticSearchOptions> optionsBuilder)
        {
            services.AddScoped<IUserActivityRepository, UserActivityRepository>();
            var options = new ElasticSearchOptions();
            optionsBuilder?.Invoke(options);
            services.AddSingleton(options);
        }
    }
}
