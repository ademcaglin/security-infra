using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.IdentityServer.Web.Infrastructure.Validators
{
    public interface IIpAddressValidator
    {
        bool IsIpAddressInternal();
    }
}
