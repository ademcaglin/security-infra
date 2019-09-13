using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Queries.ApiResources
{
    public interface IApiResourceQueryService
    {
        Task<ApiResourcePaginatedList> GetPaginatedList(ApiResourceListQuery query);
    }
}
