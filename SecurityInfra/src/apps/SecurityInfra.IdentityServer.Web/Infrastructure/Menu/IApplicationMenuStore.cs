using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecurityInfra.IdentityServer.Web.Infrastructure.Menu
{
    public interface IApplicationMenuStore
    {
        Task<IList<ApplicationMenu>> GetApplicationMenus(ClaimsPrincipal principal);
    }
}
