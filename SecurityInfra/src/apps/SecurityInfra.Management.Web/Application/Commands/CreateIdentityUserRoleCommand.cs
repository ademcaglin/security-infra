using MediatR;
using SecurityInfra.Common.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Management.Web.Application.Commands
{
    public class CreateIdentityUserRoleCommand : IRequest<CommandResponse>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

        public string TenantId { get; set; }

        public string DepartmentType { get; set; }

        public string DepartmentValue { get; set; }

        public string Rule { get; set; }
    }
}
