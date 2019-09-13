using MediatR;
using SecurityInfra.Common.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Management.Web.Application.Commands
{
    public class DisableIdentityUserRoleCommand : IRequest<CommandResponse>
    {
        public string Id { get; set; }

    }
}
