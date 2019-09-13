using AutoMapper;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SecurityInfra.Common;
using SecurityInfra.Identity.IdentityUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SecurityInfra.IdentityServer.Web.Infrastructure
{
    public class ProfileService : IProfileService
    {
        private readonly IMapper _mapper;
        private readonly IIdentityUserRepository _identityUserRepository;

        public ProfileService(IMapper mapper, IIdentityUserRepository identityUserRepository)
        {
            _mapper = mapper;
            _identityUserRepository = identityUserRepository;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var claims = new List<Claim>();
            claims.AddRange(context.Subject.Claims);
            string tenantId = "";
            context.Client.Properties.TryGetValue(SecurityInfraClaimTypes.TenantId, out tenantId);
            var userId = context.Subject.Identity.GetSubjectId();
            var user = await _identityUserRepository.GetById(userId);
            if(user != null && user.State == IdentityUserState.VALID)
            {
                var roles = user.GetCurrentRoles()
                    .Where(x => x.TenantId == tenantId &&
                           x.State == IdentityUserRoleState.VALID);
                foreach(var role in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, role.Role));
                    if (!string.IsNullOrEmpty(role.DepartmentType) ||
                        !string.IsNullOrEmpty(role.Rule))
                    {
                        dynamic roleExt = new JObject();
                        roleExt.name = role.Role;
                        if (!string.IsNullOrEmpty(role.DepartmentType))
                        {
                            roleExt.department_type = role.DepartmentType;
                            roleExt.department_value = role.DepartmentValue;
                        }
                        if (!string.IsNullOrEmpty(role.Rule))
                        {
                            roleExt.rule = role.Rule;
                        }
                        claims.Add(new Claim("role_ext", roleExt.ToString()));
                    }
                }
            }
            claims.Add(new Claim(SecurityInfraClaimTypes.TenantId, tenantId));
            context.IssuedClaims = claims;
            await Task.CompletedTask;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            await Task.CompletedTask;
        }
    }
}
