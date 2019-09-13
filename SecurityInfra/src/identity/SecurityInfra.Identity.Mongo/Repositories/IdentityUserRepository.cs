using MongoDB.Driver;
using SecurityInfra.Identity.IdentityUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInfra.Identity.Mongo.Repositories
{
    public class IdentityUserRepository : IIdentityUserRepository
    {
        private readonly ConfigurationDbContext _context;

        public IdentityUserRepository(ConfigurationDbContext context)
        {
            _context = context;
        }

        public Task<IList<IdentityUser>> GetAll()
        {
            var users = _context.IdentityUsers
                 .AsQueryable()
                 .ToList();
            return Task.FromResult<IList<IdentityUser>>(users);
        }

        public Task<IList<IdentityUser>> GetAllByDepartment(string departmentType, string departmentValue)
        {
            var users = _context.IdentityUsers
                .AsQueryable()
                .Where(x=> x.Roles.Any(y=> y.DepartmentType == departmentType))
                .ToList();
            return Task.FromResult<IList<IdentityUser>>(users);
        }

        public Task<IList<IdentityUser>> GetAllByTenantId(string tenantId)
        {
            var users = _context.IdentityUsers
                .AsQueryable()
                .Where(x => x.Roles.Any(y => y.TenantId == tenantId))
                .ToList();
            return Task.FromResult<IList<IdentityUser>>(users);
        }

        public Task<IdentityUser> GetById(string id)
        {
            var t = _context.IdentityUsers
                 .AsQueryable()
                 .Where(x => x.Id == id)
                 .FirstOrDefault();
            return Task.FromResult(t);
        }

        public async Task Save(IdentityUser t)
        {
            var replaceOneResult = await _context.IdentityUsers.ReplaceOneAsync(
                a => a.Id == t.Id, t, new UpdateOptions { IsUpsert = true });
        }
    }
}
