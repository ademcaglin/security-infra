using AutoMapper;
using SecurityInfra.Common.Cqrs;
using SecurityInfra.Configuration.Clients;
using SecurityInfra.Configuration.Web.Application.Factories;
using SecurityInfra.Configuration.Web.Application.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Commands
{
    public class CreateOrUpdateClientCommandHandler
    {
        private readonly ClientFactory _factory;
        private readonly IClientRepository _repository;

        public CreateOrUpdateClientCommandHandler(ClientFactory factory,
            IClientRepository clientRepository)
        {
            _factory = factory;
            _repository = clientRepository;
        }
        public async Task<CommandResponse> Handle(CreateOrUpdateClientCommand request, CancellationToken cancellationToken)
        {
            var c = await _factory.Create(request);
            await _repository.Save(c);
            var response = new CommandResponse();
            response.IsSucceed = true;
            response.IntegrationEvents.Add(new ClientChangedIntegrationEvent()
            {
                ClientId = c.ClientId
            });
            return response;
        }


    }
}
