using MediatR;
using SecurityInfra.Common.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Common.Cqrs
{
    public class CommandResponse
    {
        public bool IsSucceed { get; set; }

        public IList<IntegrationEvent> IntegrationEvents { get; set; } = new List<IntegrationEvent>();
    }
}
