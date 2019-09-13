using IdentityModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SecurityInfra.IdentityServer.Web.Infrastructure.Ldap
{
    public class LdapAuthenticationService : ILdapAuthenticationService
    {
        private readonly LdapOptions _options;

        public LdapAuthenticationService(IOptions<LdapOptions> options)
        {
            _options = options.Value;
        }

        public LdapAuthenticationResult AuthenticateUser(string userName, string password)
        {
            using (var cn = new LdapConnection())
            {
                cn.Connect(_options.Host, 389);
                try
                {
                    cn.Bind($"{_options.Dn}\\" + userName, password);
                }
                catch
                {
                    return LdapAuthenticationResult.Fail("USERANDPASSWORDDOESNTMATCH",
                        "User and password doesn't match");
                }

                var searchFilter = string.Format(_options.SearchFilter, userName);
                LdapSearchResults lsc = cn.Search(_options.Base,
                                                LdapConnection.SCOPE_SUB,
                                                searchFilter,
                                                null,
                                                false);

                var entry = lsc.Next();
                if (entry == null)
                {
                    return LdapAuthenticationResult.Fail("ENTRYNOTFOUND", "Entry not found");
                }
                try
                {
                   
                    var sub = entry.getAttribute(_options.SubjectAttr)?.StringValue;
                    if (string.IsNullOrEmpty(sub))
                    {
                        return LdapAuthenticationResult.Fail("SUBNOTFOUND", "The user has not a subject");
                    }
                    else
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(JwtClaimTypes.Subject, sub));
                        claims.Add(new Claim("ldap_accountname", userName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, sub));
                        var identity = new ClaimsIdentity(claims, "LDAP");
                        identity.AddClaims(claims);
                        var principal = new ClaimsPrincipal(identity);
                        return LdapAuthenticationResult.Success(principal);
                    }
                }
                catch
                {
                    return LdapAuthenticationResult.Fail("ERROR", "En error occured");
                }
            }
        }
    }

}




