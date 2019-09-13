using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecurityInfra.IdentityServer.Web.Infrastructure.Ldap
{
    public class LdapAuthenticationResult
    {
        public bool IsSucceed { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public ClaimsPrincipal Principal { get; set; }

        public static LdapAuthenticationResult Success(ClaimsPrincipal principal)
        {
            return new LdapAuthenticationResult()
            {
                IsSucceed = true,
                Principal = principal
            };
        }

        public static LdapAuthenticationResult Fail(string errorCode, string errorMessage)
        {
            return new LdapAuthenticationResult()
            {
                IsSucceed = false,
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            };
        }
    }
}
