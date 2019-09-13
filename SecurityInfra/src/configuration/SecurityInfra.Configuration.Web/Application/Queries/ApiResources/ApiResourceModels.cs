using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Queries.ApiResources

{
    public class ApiResourceListItem
    {
        public string Id { get; set; }

        public string Uri { get; set; }

        public string Title { get; set; }

        public bool Enabled { get; set; }
    }

    public class ApiResourceListQuery : ListQuery { }

    public class ApiResourcePaginatedList : PaginatedResult<ApiResourceListItem> { }
}
