using MongoDB.Driver;
using SecurityInfra.Configuration.IdentityResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Mongo.Repositories
{
    public class IdentityResourceRepository : IIdentityResourceRepository
    {
        private readonly ConfigurationDbContext _context;

        public IdentityResourceRepository(ConfigurationDbContext context)
        {
            _context = context;
        }

        public Task<IList<IdentityResource>> GetAll()
        {
            var identityResources = _context.IdentityResources
                .AsQueryable()
                .ToList();
            return Task.FromResult<IList<IdentityResource>>(identityResources);
        }

        public Task<IdentityResource> GetById(string id)
        {
            var identityResource = _context.IdentityResources
                 .AsQueryable()
                 .Where(x => x.Id == id)
                 .FirstOrDefault();
            return Task.FromResult(identityResource);
        }

        public Task<IdentityResource> GetByName(string name)
        {
            var identityResource = _context.IdentityResources
                 .AsQueryable()
                 .Where(x => x.Name == name)
                 .FirstOrDefault();
            return Task.FromResult(identityResource);
        }

        public async Task Save(IdentityResource identityResource)
        {
            var replaceOneResult = await _context.IdentityResources.ReplaceOneAsync(
               c => c.Name == identityResource.Name, identityResource, new UpdateOptions { IsUpsert = true });
        }
    }
}
