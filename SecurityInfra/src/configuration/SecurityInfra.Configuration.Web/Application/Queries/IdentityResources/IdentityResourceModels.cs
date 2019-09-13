using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Queries.IdentityResources
{
    public class IdentityResourceListItem
    {
        public string Id { get; set; }

        public string Uri { get; set; }

        public string Title { get; set; }

        public bool Enabled { get; set; }
    }

    public class IdentityResourceListQuery : ListQuery { }

    public class IdentityResourcePaginatedList : PaginatedResult<IdentityResourceListItem> { }
}
