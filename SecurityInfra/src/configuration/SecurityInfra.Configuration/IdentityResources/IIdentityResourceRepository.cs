using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.IdentityResources
{
    public interface IIdentityResourceRepository
    {
        Task<IdentityResource> GetById(string id);

        Task<IdentityResource> GetByName(string name);

        Task<IList<IdentityResource>> GetAll();

        Task Save(IdentityResource identityResource);
    }
}
