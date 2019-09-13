using AutoMapper;
using SecurityInfra.Configuration.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecurityInfra.Configuration.Web.Application.Commands;
using SecurityInfra.Configuration.IdentityResources;

namespace SecurityInfra.Configuration.Web.Application.Mappers
{
    public class IdentityResourceFactory
    {
        private readonly IIdentityResourceRepository _repository;
        public IdentityResourceFactory(IIdentityResourceRepository repository)
        {
            _repository = repository;
        }
        public async Task<IdentityResource> Map(CreateOrUpdateIdentityResourceCommand command)
        {
            var identityResource = Mapper.Map<CreateOrUpdateIdentityResourceCommand, IdentityResource>(command);
            if (string.IsNullOrEmpty(command.Id))
            {
                identityResource.Id = Guid.NewGuid().ToString();
            }
            else
            {
                var exist = await _repository.GetById(identityResource.Id);
                if (!exist.Enabled)
                {
                    throw new Exception("IdentityResource can't be enabled with this command.");
                }
                identityResource.Name = exist.Name;
            }
            return identityResource;
        }
    }
}
