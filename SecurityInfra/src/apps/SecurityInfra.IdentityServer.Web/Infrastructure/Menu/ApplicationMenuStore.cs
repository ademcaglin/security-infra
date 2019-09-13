using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecurityInfra.IdentityServer.Web.Infrastructure.Menu
{
    public class ApplicationMenuStore : IApplicationMenuStore
    {
        public Task<IList<ApplicationMenu>> GetApplicationMenus(ClaimsPrincipal principal)
        {
            // if cached then get 
            // else get from db and cache it
            throw new NotImplementedException();
        }
    }
}
