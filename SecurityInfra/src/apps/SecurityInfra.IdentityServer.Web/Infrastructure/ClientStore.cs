using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using AutoMapper;
using SecurityInfra.Common;

namespace SecurityInfra.IdentityServer.Web.Infrastructure
{
    public class ClientStore : IClientStore
    {
        private readonly ILogger _logger;
        private readonly Configuration.Clients.IClientRepository _clientService;
        private readonly IMapper _mapper;

        public ClientStore(Configuration.ApiResources.IApiResourceRepository apiResourceService,
            Configuration.Clients.IClientRepository clientService,
            ILogger<ResourceStore> logger,
            IMapper mapper)
        {
            _clientService = clientService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var clientEntity = await _clientService.GetById(clientId);
            var client = _mapper.Map<Client>(clientEntity);
            if (client != null)
            {
                client.Claims.Add(new System.Security.Claims.Claim(SecurityInfraClaimTypes.TenantId, clientEntity.TenantId));
            }
            return client;
        }
    }
}