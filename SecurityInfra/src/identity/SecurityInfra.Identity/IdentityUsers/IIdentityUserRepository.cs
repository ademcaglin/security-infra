using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInfra.Identity.IdentityUsers
{
    public interface IIdentityUserRepository
    {
        Task<IdentityUser> GetById(string citizenId);

        Task<IList<IdentityUser>> GetAll();

        Task<IList<IdentityUser>> GetAllByTenantId(string tenantId);

        Task<IList<IdentityUser>> GetAllByDepartment(string departmentType, string departmentValue);

        Task Save(IdentityUser user);
    }
}
