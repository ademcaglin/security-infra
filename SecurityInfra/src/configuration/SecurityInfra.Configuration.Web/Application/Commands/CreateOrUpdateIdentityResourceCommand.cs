using MediatR;
using SecurityInfra.Common.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Commands
{
    public class CreateOrUpdateIdentityResourceCommand : IRequest<CommandResponse>
    {
        public string Id { get; private set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public ICollection<string> UserClaims { get; set; } = new HashSet<string>();

        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

    }
}
