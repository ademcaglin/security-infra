using MediatR;
using SecurityInfra.Common.Cqrs;
using SecurityInfra.Common.EventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityInfra.Management.Web.Application.Commands
{
    public class CreateIdentityUserRoleCommandHandler : IRequestHandler<CreateIdentityUserRoleCommand, CommandResponse>
    {
        //private readonly IMenuProviderRepository _menuProviderRepository;

        //public CreateIdentityUserRoleCommandHandler(IMenuProviderRepository menuProviderRepository)
        //{
        //    _menuProviderRepository = menuProviderRepository;
        //}

        public async Task<CommandResponse> Handle(CreateIdentityUserRoleCommand request, CancellationToken cancellationToken)
        {
            //var pr = new MenuProvider(request.Uri, request.Title);
            //await _menuProviderRepository.Save(pr);
            //var r = new CommandResponse();
            //r.IntegrationEvents.Add(new MenuProvidersChangedIntegrationEvent());
            //r.IsSucceed = true;
            //return r;
            return null;
        }
    }
}
