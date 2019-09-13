using AutoMapper;
using MongoDB.Driver;
using SecurityInfra.Identity.IdentityUsers;
using SecurityInfra.Management.Web.Application.Queries.IdentityUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Management.Web.Application.Queries.MenuProviders
{
    public class IdentityUserQueryService : IIdentityUserQueryService
    {
        private readonly Identity.Mongo.ConfigurationDbContext _context;
        private readonly IMapper _mapper;
        public IdentityUserQueryService(Identity.Mongo.ConfigurationDbContext context,
            IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public Task<IdentityUserPaginatedList> GetPaginatedList(IdentityUserListQuery query)
        {
            var paginatedList = new IdentityUserPaginatedList();
            var queryable = _context.IdentityUsers.AsQueryable();
            if (!string.IsNullOrEmpty(query.SearchText))
            {
                queryable.Where(x => x.FirstName.Contains(query.SearchText));
            }
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var camelCase = Char.ToUpper(query.SortBy[0]) + query.SortBy.Substring(1);
                var propertyInfo = typeof(IdentityUserListItem).GetProperty(camelCase);
                if (query.Descending)
                {
                    queryable.OrderBy(x => propertyInfo.GetValue(x, null));
                }
                else
                {
                    queryable.OrderByDescending(x => propertyInfo.GetValue(x, null));
                }
            }

            paginatedList.Data = queryable
                            .Skip((query.Page - 1) * query.RowsPerPage)
                            .Take(query.RowsPerPage)
                            .Select(x=> _mapper.Map<IdentityUserListItem>(x))
                            .ToList();
            paginatedList.TotalRecords = queryable.Count();
            return Task.FromResult(paginatedList);
        }
    }
}
