using MediatR;
using SecurityInfra.Common.Cqrs;
using SecurityInfra.Common.EventBus.Abstractions;
using SecurityInfra.Configuration.MenuProviders;
using SecurityInfra.Configuration.Web.Application.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Commands
{
    public class CreateOrUpdateMenuProviderCommandHandler : IRequestHandler<CreateOrUpdateMenuProviderCommand, CommandResponse>
    {
        private readonly IMenuProviderRepository _menuProviderRepository;

        public CreateOrUpdateMenuProviderCommandHandler(IMenuProviderRepository menuProviderRepository)
        {
            _menuProviderRepository = menuProviderRepository;
        }

        public async Task<CommandResponse> Handle(CreateOrUpdateMenuProviderCommand request, CancellationToken cancellationToken)
        {
            var pr = new MenuProvider(request.Uri, request.Title);
            await _menuProviderRepository.Save(pr);
            var r = new CommandResponse();
            r.IntegrationEvents.Add(new MenuProvidersChangedIntegrationEvent());
            r.IsSucceed = true;
            return r;
        }
    }
}
