using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Infrastructure.RedisCache
{
    public class RedisCacheOptions
    {
        private string _keyPrefix = string.Empty;

        public string KeyPrefix
        {
            get
            {
                return string.IsNullOrEmpty(this._keyPrefix) ? this._keyPrefix : $"{_keyPrefix}:";
            }
            set
            {
                this._keyPrefix = value;
            }
        }

        public string RedisConnectionString { get; set; }
    }
}
