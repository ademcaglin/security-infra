using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Common.UserActivity
{
    public class UserActivityEvent : EventBus.Events.IntegrationEvent
    {
        public string JwtToken { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string ActionName { get; set; }

        public string ActionTitle { get; set; }

        public string UserIpAddress { get; set; }

        public string RequestId { get; set; }

        public string Data { get; set; }
    }
}
