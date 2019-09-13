using MongoDB.Driver;
using SecurityInfra.Configuration.MenuProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Mongo.Repositories
{
    public class MenuProviderRepository : IMenuProviderRepository
    {
        private readonly ConfigurationDbContext _context;

        public MenuProviderRepository(ConfigurationDbContext context)
        {
            _context = context;
        }

        public Task<IList<MenuProvider>> GetAll()
        {
            var menuProviders = _context.MenuProviders
                .AsQueryable()
                .ToList();
            return Task.FromResult<IList<MenuProvider>>(menuProviders);
        }

        public Task<MenuProvider> GetById(string id)
        {
            var menuProvider = _context.MenuProviders
                 .AsQueryable()
                 .Where(x => x.Id == id)
                 .FirstOrDefault();
            return Task.FromResult(menuProvider);
        }

        public async Task Save(MenuProvider menuProvider)
        {
            var replaceOneResult = await _context.MenuProviders.ReplaceOneAsync(
              c => c.Id == menuProvider.Id, menuProvider, new UpdateOptions { IsUpsert = true });
        }
    }
}
