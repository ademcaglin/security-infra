using IdentityModel.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Options;
using SecurityInfra.Common.Authentication;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace SecurityInfra.Common.Tests
{
    public class JwtTokenValidatorTests
    {
        private const string TokenEndpoint = "https://server/connect/token";
        private readonly HttpClient _openIdClient;

        public JwtTokenValidatorTests()
        {
            var openIdBuilder = new WebHostBuilder()
                .UseStartup<Setup.OpenIdConnectStartup>();
            var openIdServer = new TestServer(openIdBuilder);
            
            _openIdClient = openIdServer.CreateClient();
        }

        [Fact]
        public void JwtToken_Should_Be_Validated()
        {
            var response = _openIdClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "/connect/token",
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            }).Result;

            string token = response.AccessToken;
            var validator = new JwtTokenValidator(Options.Create<JwtTokenOptions>(new JwtTokenOptions()
            {
                Authority = "https://server"
            }), _openIdClient);
            var p = validator.Validate(token).Result;
            Assert.NotNull(p.FindFirst("iss"));
        }

    }
}
