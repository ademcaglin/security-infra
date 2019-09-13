using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Clients
{
    public interface IClientRepository
    {
        Task<Client> GetById(string id);

        Task<Client> GetByClientId(string clientId);

        Task<IList<Client>> GetAll();

        Task Save(Client client);
    }
}
