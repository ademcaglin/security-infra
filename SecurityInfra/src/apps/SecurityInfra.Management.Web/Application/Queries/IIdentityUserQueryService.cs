using SecurityInfra.Management.Web.Application.Queries.IdentityUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Management.Web.Application.Queries.MenuProviders
{
    public interface IIdentityUserQueryService
    {
        Task<IdentityUserPaginatedList> GetPaginatedList(IdentityUserListQuery query);
    }
}
