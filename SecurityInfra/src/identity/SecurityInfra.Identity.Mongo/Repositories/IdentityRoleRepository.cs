using MongoDB.Driver;
using SecurityInfra.Identity.IdentityRoles;
using SecurityInfra.Identity.IdentityUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInfra.Identity.Mongo.Repositories
{
    public class IdentityRoleRepository : IIdentityRoleRepository
    {
        private readonly ConfigurationDbContext _context;

        public IdentityRoleRepository(ConfigurationDbContext context)
        {
            _context = context;
        }

        public Task<IList<IdentityRole>> GetAll()
        {
            var users = _context.IdentityRoles
                 .AsQueryable()
                 .ToList();
            return Task.FromResult<IList<IdentityRole>>(users);
        }
    }
}
