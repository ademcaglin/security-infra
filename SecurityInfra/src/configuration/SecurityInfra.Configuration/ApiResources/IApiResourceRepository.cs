using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.ApiResources
{
    public interface IApiResourceRepository
    {
        Task<ApiResource> GetById(string id);

        Task<ApiResource> GetByName(string name);

        Task<IList<ApiResource>> GetAllByScopes(IEnumerable<string> scopeNames);

        Task<IList<ApiResource>> GetAll();

        Task Save(ApiResource apiResource);
    }
}
