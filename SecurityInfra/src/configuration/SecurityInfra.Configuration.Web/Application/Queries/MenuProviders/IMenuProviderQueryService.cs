using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Queries.MenuProviders
{
    public interface IMenuProviderQueryService
    {
        Task<MenuProviderPaginatedList> GetPaginatedList(MenuProviderListQuery query);
    }
}
