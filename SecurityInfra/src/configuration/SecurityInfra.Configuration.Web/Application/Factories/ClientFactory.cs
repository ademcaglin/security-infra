using AutoMapper;
using SecurityInfra.Configuration.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecurityInfra.Configuration.Web.Application.Commands;

namespace SecurityInfra.Configuration.Web.Application.Factories
{
    public class ClientFactory
    {
        private readonly IClientRepository _repository;
        public ClientFactory(IClientRepository repository)
        {
            _repository = repository;
        }
        public async Task<Client> Create(CreateOrUpdateClientCommand command)
        {
            var c = Mapper.Map<CreateOrUpdateClientCommand, Client>(command);
            var exist = await _repository.GetByClientId(c.ClientId);
            if (!exist.Enabled)
            {
                throw new Exception("Client can't be enabled with this command.");
            }
            if (string.IsNullOrEmpty(command.Id) && exist == null)
            {
                c.Id = Guid.NewGuid().ToString();
            }
            return c;
        }
    }
}
