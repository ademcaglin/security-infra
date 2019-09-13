using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.IdentityServer.Web.Infrastructure.Ldap
{
    public interface ILdapAuthenticationService
    {
        LdapAuthenticationResult AuthenticateUser(string userName, string password);
    }
}
