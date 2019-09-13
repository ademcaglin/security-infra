using IdentityModel;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using Newtonsoft.Json;
using SecurityInfra.Common;

namespace System.Security.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal p)
        {
            return p.FindFirst(JwtClaimTypes.Subject).Value;
        }

        //public static IList<AccessRule> GetAccessRules(this ClaimsPrincipal p)
        //{
        //    return p.FindAll(SecurityInfraClaimTypes.RoleAccessRule)
        //     .Select(x => JsonConvert.DeserializeObject<AccessRule>(x.Value))
        //     .ToList();
        //}
    }
}
