using SecurityInfra.Common.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.IntegrationEvents
{
    public class ApiResourceChangedIntegrationEvent : IntegrationEvent
    {
        public string ApiResourceName { get; set; }
    }
}
