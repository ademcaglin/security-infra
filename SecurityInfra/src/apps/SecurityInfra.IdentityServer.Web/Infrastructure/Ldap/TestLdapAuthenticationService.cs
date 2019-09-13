using IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecurityInfra.IdentityServer.Web.Infrastructure.Ldap
{
    public class TestLdapAuthenticationService : ILdapAuthenticationService
    {
        public LdapAuthenticationResult AuthenticateUser(string userName, string password)
        {
            if (userName == "testuser" && password == "testpwd")
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("ldap_accountname", userName));
                claims.Add(new Claim(JwtClaimTypes.Subject, "11111111111"));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, "11111111111"));
                claims.Add(new Claim(ClaimTypes.Name, "Test Test"));
                var identity = new ClaimsIdentity(claims, "LDAP");
                identity.AddClaims(claims);
                var principal = new ClaimsPrincipal(identity);
                return LdapAuthenticationResult.Success(principal);
            }
            else
            {
                return LdapAuthenticationResult.Fail("ENTRYNOTFOUND",
                        "User and password doesn't match");
            }
        }
    }
}
