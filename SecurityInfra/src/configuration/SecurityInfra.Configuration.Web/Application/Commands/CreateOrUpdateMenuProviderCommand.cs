using MediatR;
using SecurityInfra.Common.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Commands
{
    public class CreateOrUpdateMenuProviderCommand : IRequest<CommandResponse>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Uri { get; set; }

        public bool Enabled { get; set; }
    }
}
