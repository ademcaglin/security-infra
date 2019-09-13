using MongoDB.Driver;
using SecurityInfra.Common.Exceptions;
using SecurityInfra.Configuration.ApiResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Mongo.Repositories
{
    public class ApiResourceRepository : IApiResourceRepository
    {
        private readonly ConfigurationDbContext _context;

        public ApiResourceRepository(ConfigurationDbContext context)
        {
            _context = context;
        }

        public Task<IList<ApiResource>> GetAll()
        {
            var apiResources = _context.ApiResources
                 .AsQueryable()
                 .ToList();
            return Task.FromResult<IList<ApiResource>>(apiResources);
        }

        public Task<IList<ApiResource>> GetAllByScopes(IEnumerable<string> scopeNames)
        {
            var names = scopeNames.ToArray();
            var apiResources = _context.ApiResources
                 .AsQueryable()
                 .Where(x => x.Scopes.Where(y => names.Contains(y.Name)).Any())
                 .ToList();

            return Task.FromResult<IList<ApiResource>>(apiResources);
        }

        public Task<ApiResource> GetById(string id)
        {
            var apiResource = _context.ApiResources
                 .AsQueryable()
                 .Where(x => x.Id == id)
                 .FirstOrDefault();
            return Task.FromResult(apiResource);
        }

        public Task<ApiResource> GetByName(string name)
        {
            var apiResource = _context.ApiResources
                 .AsQueryable()
                 .Where(x => x.Name == name)
                 .FirstOrDefault();
            return Task.FromResult(apiResource);
        }

        public async Task Save(ApiResource apiResource)
        {
            var validator = new ApiResourceValidator();
            if (!validator.Validate(apiResource).IsValid)
            {
                throw new DomainValidationException("");
            }
            var replaceOneResult = await _context.ApiResources.ReplaceOneAsync(
                a => a.Name == apiResource.Name, apiResource, new UpdateOptions { IsUpsert = true });
        }
    }
}


//private readonly IdentityServerDbContext _context;

//public ClientRepository(IdentityServerDbContext context)
//{
//    _context = context;
//}

//public Task<Client> Find(string id)
//{
//    var client = _context.Clients
//         .AsQueryable()
//         .Where(x => x.Id == id)
//         .FirstOrDefault();
//    return Task.FromResult(client);
//}

//public Task<Client> FindOne(Expression<Func<Client, bool>> predicate)
//{
//    var client = _context.Clients
//        .AsQueryable()
//        .Where(predicate)
//        .FirstOrDefault();
//    return Task.FromResult(client);
//}

//public async Task<IList<Client>> Get()
//{
//    return await _context.Clients
//        .AsQueryable()
//        .ToListAsync();
//}

//public async Task<IList<Client>> Get(Expression<Func<Client, bool>> predicate)
//{
//    return await _context.Clients
//       .AsQueryable()
//       .Where(predicate)
//       .ToListAsync();
//}

//public async Task Save(Client client)
//{
//    var replaceOneResult = await _context.Clients.ReplaceOneAsync(
//        c => c.Id == client.Id, client, new UpdateOptions { IsUpsert = true });
//}