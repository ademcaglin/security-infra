using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Identity.IdentityRoles
{
    public class IdentityRole
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string TenantId { get; set; }

        public string Title { get; set; }

        public bool HasRule { get; set; }

        public string ResourceType { get; set; }

        public bool Enabled { get; set; } = true;
    }
}
