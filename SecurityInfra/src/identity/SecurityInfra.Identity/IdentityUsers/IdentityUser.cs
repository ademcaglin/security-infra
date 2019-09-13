using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
namespace SecurityInfra.Identity.IdentityUsers
{
    public class IdentityUser
    {
        protected IdentityUser()
        {

        }

        public IdentityUser(string citizenId, string createdBy, string firstName, string lastName)
        {
            Id = Guid.NewGuid().ToString();
            CitizenId = citizenId;
            CreatedAt = DateTime.Now;
            CreatedBy = createdBy;
            FirstName = firstName;
            LastName = lastName;
        }

        public string Id { get; private set; }

        public string CitizenId { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public string CreatedBy { get; private set; }

        public DateTime? DisabledAt { get; private set; }

        public string DisabledBy { get; private set; }

        public IdentityUserState State { get; private set; } = IdentityUserState.VALID;

        public bool IsDeleted { get; private set; } = false;

        public IList<IdentityUserRole> Roles { get; private set; } = new List<IdentityUserRole>();

        public List<KeyValuePair<string, string>> Claims { get; private set; } = new List<KeyValuePair<string, string>>();

        public IList<IdentityUserRole> GetValidRoles()
        {
            var roles = Roles.Where(x => x.State == IdentityUserRoleState.VALID &&
                                   (!x.DateOfEnd.HasValue || x.DateOfEnd.Value > DateTime.Now))
                        .ToList();
            return roles;
        }

        public void Disable()
        {
        }
    }
}
