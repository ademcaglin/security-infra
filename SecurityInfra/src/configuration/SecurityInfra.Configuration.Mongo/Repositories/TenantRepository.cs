using MongoDB.Driver;
using SecurityInfra.Configuration.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Mongo.Repositories
{
    public class TenantRepository : ITenentRepository
    {
        private readonly ConfigurationDbContext _context;

        public TenantRepository(ConfigurationDbContext context)
        {
            _context = context;
        }

        public Task<IList<Tenant>> GetAll()
        {
            var tenants = _context.Tenants
                 .AsQueryable()
                 .ToList();
            return Task.FromResult<IList<Tenant>>(tenants);
        }


        public Task<Tenant> GetById(string id)
        {
            var t = _context.Tenants
                 .AsQueryable()
                 .Where(x => x.Id == id)
                 .FirstOrDefault();
            return Task.FromResult(t);
        }

        public async Task Save(Tenant t)
        {
            var replaceOneResult = await _context.Tenants.ReplaceOneAsync(
                a => a.Id == t.Id, t , new UpdateOptions { IsUpsert = true });
        }
    }
}
