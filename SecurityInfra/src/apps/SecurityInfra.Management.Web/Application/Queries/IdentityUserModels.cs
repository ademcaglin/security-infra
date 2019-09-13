using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Management.Web.Application.Queries.IdentityUsers
{
    public class IdentityUserListItem
    {
        public string Id { get; set; }

        public string CitizenId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        
    }

    public class IdentityUserListQuery : ListQuery { }

    public class IdentityUserPaginatedList : PaginatedResult<IdentityUserListItem> { }
}
