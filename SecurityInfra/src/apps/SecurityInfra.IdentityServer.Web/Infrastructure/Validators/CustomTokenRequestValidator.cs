using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.IdentityServer.Web.Infrastructure.Validators
{
    internal class CustomTokenRequestValidator : ICustomTokenRequestValidator
    {
        private readonly IIpAddressValidator _ipAddressValidator;
        public CustomTokenRequestValidator(IIpAddressValidator ipAddressValidator)
        {
            _ipAddressValidator = ipAddressValidator;
        }
        public Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            var hasClientCredentials = context.Result.ValidatedRequest.Client.AllowedGrantTypes.Contains("client_credentials");

            if (hasClientCredentials && !_ipAddressValidator.IsIpAddressInternal())
            {
                context.Result.IsError = true;
                context.Result.Error = OidcConstants.AuthorizeErrors.UnauthorizedClient;
            }
            return Task.CompletedTask;
        }
    }
}
