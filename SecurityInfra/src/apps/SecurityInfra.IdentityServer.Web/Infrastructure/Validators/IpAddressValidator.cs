using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.IdentityServer.Web.Infrastructure.Validators
{
    public class IpAddressValidator : IIpAddressValidator
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IpAddressValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public bool IsIpAddressInternal()
        {
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            return true;
            //return ip.ToString().StartsWith("10.");
        }
    }
}
