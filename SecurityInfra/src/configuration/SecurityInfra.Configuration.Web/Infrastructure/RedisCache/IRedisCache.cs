using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Infrastructure.RedisCache
{
    public interface IRedisCache<T>
    {
        Task RemoveAsync(string key);

        Task RemoveAllAsync();
    }
}
