using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecurityInfra.Common.Authentication
{
    public interface IJwtTokenValidator
    {
        Task<ClaimsPrincipal> Validate(string token);
    }
}
