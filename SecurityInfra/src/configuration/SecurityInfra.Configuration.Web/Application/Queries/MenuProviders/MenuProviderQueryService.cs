using AutoMapper;
using MongoDB.Driver;
using SecurityInfra.Configuration.MenuProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Queries.MenuProviders
{
    public class MenuProviderQueryService : IMenuProviderQueryService
    {
        private readonly Mongo.ConfigurationDbContext _context;
        private readonly IMapper _mapper;
        public MenuProviderQueryService(Mongo.ConfigurationDbContext context,
            IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public Task<MenuProviderPaginatedList> GetPaginatedList(MenuProviderListQuery query)
        {
            var paginatedList = new MenuProviderPaginatedList();
            var queryable = _context.MenuProviders.AsQueryable();
            if (!string.IsNullOrEmpty(query.SearchText))
            {
                queryable.Where(x => x.Title.Contains(query.SearchText));
            }
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var camelCase = Char.ToUpper(query.SortBy[0]) + query.SortBy.Substring(1);
                var propertyInfo = typeof(MenuProviderListItem).GetProperty(camelCase);
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
                            .Select(x=> _mapper.Map<MenuProviderListItem>(x))
                            .ToList();
            paginatedList.TotalRecords = queryable.Count();
            return Task.FromResult(paginatedList);
        }
    }
}
