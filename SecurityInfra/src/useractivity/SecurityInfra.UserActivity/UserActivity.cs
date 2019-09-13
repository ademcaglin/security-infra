using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.UserActivity
{
    public class UserActivity
    {
        public string Id { get; set; }

        public string TenantId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string ActionName { get; set; }

        public string ActionTitle { get; set; }

        public string UserIpAddress { get; set; }

        public string RequestId { get; set; }

        public string Message { get; set; }

        public IDictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
    }
}
