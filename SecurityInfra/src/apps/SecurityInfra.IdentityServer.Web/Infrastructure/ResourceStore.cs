using AutoMapper;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.IdentityServer.Web.Infrastructure
{
    public class ResourceStore : IResourceStore
    {
        private readonly ILogger _logger;
        private readonly Configuration.ApiResources.IApiResourceRepository _apiResourceService;
        private readonly Configuration.IdentityResources.IIdentityResourceRepository _identityResourceService;
        private readonly IMapper _mapper;

        public ResourceStore(Configuration.ApiResources.IApiResourceRepository apiResourceService,
            Configuration.IdentityResources.IIdentityResourceRepository identityResourceService,
            ILogger<ResourceStore> logger,
            IMapper mapper)
        {
            _apiResourceService = apiResourceService;
            _identityResourceService = identityResourceService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            var api = await _apiResourceService.GetByName(name);

            if (api != null)
            {
                _logger.LogDebug("Found {api} API resource in database", name);
            }
            else
            {
                _logger.LogDebug("Did not find {api} API resource in database", name);
            }

            return _mapper.Map<ApiResource>(api);
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var apis = await _apiResourceService.GetAllByScopes(scopeNames);

            var models = apis.Select(x => _mapper.Map<ApiResource>(x)).ToArray();

            _logger.LogDebug("Found {scopes} API scopes in database",
                models.SelectMany(x => x.Scopes).Select(x => x.Name));

            return models.AsEnumerable();
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var all = await _identityResourceService.GetAll();
            var identities = all.Where(x => scopeNames.Contains(x.Name));

            var models = identities.Select(x => _mapper.Map<IdentityResource>(x)).ToArray();

            return models.AsEnumerable();
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            var apis = await _apiResourceService.GetAll();
            var identities = await _identityResourceService.GetAll();
            var result = new Resources(
                identities.Select(x => _mapper.Map<IdentityResource>(x)).AsEnumerable(),
                apis.Select(x => _mapper.Map<ApiResource>(x)).AsEnumerable());

            _logger.LogDebug("Found {scopes} as all scopes in database",
                result.IdentityResources.Select(x => x.Name).Union(result.ApiResources.SelectMany(x => x.Scopes).Select(x => x.Name)));

            return result;
        }
    }
}
