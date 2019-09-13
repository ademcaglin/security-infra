using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.IdentityServer.Web.Infrastructure.Ldap
{
    public class LdapOptions
    {
        public string Host { get; set; }

        public string Dn { get; set; }

        public string Base { get; set; }

        public string SearchFilter { get; set; }

        public string SubjectAttr { get; set; }
    }
}
