using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecurityInfra.Configuration.ApiResources;
using SecurityInfra.Configuration.IdentityResources;
using SecurityInfra.Configuration.MenuProviders;
using SecurityInfra.Configuration.Clients;

namespace SecurityInfra.Configuration.Web.Application
{
    public class Seed
    {
        private readonly IClientRepository _clientRepository;
        private readonly IApiResourceRepository _apiResourceRepository;
        private readonly IIdentityResourceRepository _identityResourceRepository;
        private readonly IMenuProviderRepository _menuProviderRepository;

        public Seed(IClientRepository clientRepository,
            IApiResourceRepository apiResourceRepository,
            IIdentityResourceRepository identityResourceRepository,
            IMenuProviderRepository menuProviderRepository)
        {
            _clientRepository = clientRepository;
            _apiResourceRepository = apiResourceRepository;
            _identityResourceRepository = identityResourceRepository;
            _menuProviderRepository = menuProviderRepository;
        }

        public async Task Init()
        {
            //var jsClient = new Client("authority_jsclient", "authority");
            //jsClient.AccessTokenType = AccessTokenType.Reference;
            //jsClient.AllowAccessTokensViaBrowser = true;
            //jsClient.ClientName = "Authority Javascript Client";
            //jsClient.RedirectUris.Add("");
            //await _clientRepository.Save(jsClient);

            //var authorityServerClient = new Client( "authority_jsclient", "authority");
            //authorityServerClient.AccessTokenType = AccessTokenType.Jwt;
            //authorityServerClient.ClientName = "Authority Javascript Client";
            //authorityServerClient.RedirectUris.Add("");

            var configurationApiResource = new ApiResource();

            //var openId = new IdentityResource();

            //var profile = new IdentityResource();

            var menuProvider = new MenuProvider("", "");
        }
    }
}
