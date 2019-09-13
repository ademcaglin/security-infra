using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Queries
{
    public abstract class ListQuery
    {
        public int Page { get; set; }

        public int RowsPerPage { get; set; }

        public string SortBy { get; set; }

        public bool Descending { get; set; }

        public string SearchText { get; set; }
    }
}
