using MongoDB.Driver;
using SecurityInfra.Configuration.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Mongo.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ConfigurationDbContext _context;

        public ClientRepository(ConfigurationDbContext context)
        {
            _context = context;
        }
        public Task<IList<Client>> GetAll()
        {
            var clients = _context.Clients
                .AsQueryable()
                .ToList();
            return Task.FromResult<IList<Client>>(clients);
        }

        public Task<Client> GetByClientId(string clientId)
        {
            var client = _context.Clients
                .AsQueryable()
                .Where(x => x.ClientId == clientId)
                .FirstOrDefault();
            return Task.FromResult(client);
        }

        public Task<Client> GetById(string id)
        {
            var client = _context.Clients
                 .AsQueryable()
                 .Where(x => x.Id == id)
                 .FirstOrDefault();
            return Task.FromResult(client);
        }

        public async Task Save(Client client)
        {
            var replaceOneResult = await _context.Clients.ReplaceOneAsync(
                c => c.ClientId == client.ClientId, client, new UpdateOptions { IsUpsert = true });
        }
    }
}
