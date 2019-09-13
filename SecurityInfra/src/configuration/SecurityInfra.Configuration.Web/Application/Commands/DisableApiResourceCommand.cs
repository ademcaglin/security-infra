using MediatR;
using SecurityInfra.Common.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Commands
{
    public class DisableApiResourceCommand : IRequest<CommandResponse>
    {
    }
}
