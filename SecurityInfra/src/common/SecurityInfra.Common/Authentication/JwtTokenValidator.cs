using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SecurityInfra.Common.Authentication
{
    public class JwtTokenValidator : IJwtTokenValidator
    {
        private readonly JwtTokenOptions _options;
        private readonly HttpClient _httpClient;
        public JwtTokenValidator(IOptions<JwtTokenOptions> options, 
            HttpClient httpClient)
        {
            _options = options.Value;
            _httpClient = httpClient;
        }

        public async Task<ClaimsPrincipal> Validate(string token)
        {
            var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                        $"{_options.Authority}/.well-known/openid-configuration",
                        new OpenIdConnectConfigurationRetriever(),
                        new HttpDocumentRetriever(_httpClient));
           
            OpenIdConnectConfiguration openIdConfig = await configurationManager.GetConfigurationAsync(CancellationToken.None);
            var validationParameters = new TokenValidationParameters
            {
                ValidIssuer = _options.Authority,
                ValidateAudience = false,
                IssuerSigningKeys = openIdConfig.SigningKeys
            };

            SecurityToken validatedToken;
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.ValidateToken(token, validationParameters, out validatedToken);
        }
    }
}
