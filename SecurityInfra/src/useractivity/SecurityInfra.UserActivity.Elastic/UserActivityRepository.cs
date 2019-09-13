using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInfra.UserActivity.Elastic
{
    public class UserActivityRepository : IUserActivityRepository
    {
        private readonly ElasticClient _client;
        public UserActivityRepository(IOptions<ElasticSearchOptions> options)
        {
            var settings = new ConnectionSettings(new Uri(options.Value.ElasticConnectionSettings))
               .DefaultIndex("UserActivities");

            _client = new ElasticClient(settings);
        }

        public async Task Add(UserActivity activity)
        {
            await _client.IndexDocumentAsync(activity);
        }

        public IReadOnlyCollection<UserActivity> GetAllByUserId(string id)
        {
            var searchResponse = _client.Search<UserActivity>(s => s
              .Query(q => q.Match(c => c.Field(x => x.UserId).Query(id))));
            return searchResponse.Documents;
        }
    }
}
