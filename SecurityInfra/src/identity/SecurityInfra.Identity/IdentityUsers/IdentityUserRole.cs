using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SecurityInfra.Identity.IdentityUsers
{
    public class IdentityUserRole
    {
        protected IdentityUserRole()
        {

        }

        public IdentityUserRole(string tenantId, string createdBy, string role)
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            CreatedBy = createdBy;
            Role = role;
            TenantId = tenantId;
        }

        public string Id { get; private set; }

        public string TenantId { get; private set; }

        public string Role { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public string CreatedBy { get; private set; }

        public string CreatedComment { get; private set; }

        public DateTime? DisabledAt { get; set; }

        public string DisabledBy { get; private set; }

        public string DisabledComment { get; private set; }

        public IdentityUserRoleState State { get; private set; } = IdentityUserRoleState.VALID;

        public DateTime? DateOfEnd { get; private set; }

        public string DepartmentType { get; private set; }

        public string DepartmentValue { get; private set; }

        public bool IsDeleted { get; private set; } = false;

        public string Rule { get; private set; }

        public void Disable(string comment, string userId)
        {
            DisabledAt = DateTime.Now;
            DisabledBy = userId;
            DisabledComment = comment;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
