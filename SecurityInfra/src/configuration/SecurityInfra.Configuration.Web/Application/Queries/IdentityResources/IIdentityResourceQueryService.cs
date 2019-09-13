using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Queries.IdentityResources
{
    public interface IIdentityResourceQueryService
    {
        Task<IdentityResourcePaginatedList> GetPaginatedList(IdentityResourceListQuery query);
    }
}
