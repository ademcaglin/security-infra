using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Management.Web.Application.Queries
{
    public class PaginatedResult<T>
    {
        public int TotalRecords { get; set; }

        public IList<T> Data { get; set; } = new List<T>();
    }
}
