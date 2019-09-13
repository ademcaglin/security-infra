using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Tenants
{
    public interface ITenentRepository
    {
        Task<IList<Tenant>> GetAll();

        Task<Tenant> GetById(string id);

        Task Save(Tenant tenant);
    }
}
