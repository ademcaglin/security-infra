using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInfra.Identity.IdentityRoles
{
    public interface IIdentityRoleRepository
    {
        Task<IList<IdentityRole>> GetAll();
    }
}
