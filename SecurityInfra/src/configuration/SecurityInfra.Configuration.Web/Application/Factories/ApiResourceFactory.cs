using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecurityInfra.Configuration.Web.Application.Commands;
using SecurityInfra.Configuration.ApiResources;

namespace SecurityInfra.Configuration.Web.Application.Factories
{
    public class ApiResourceFactory
    {
        private readonly IApiResourceRepository _repository;
        public ApiResourceFactory(IApiResourceRepository repository)
        {
            _repository = repository;
        }
        public async Task<ApiResource> Map(CreateOrUpdateApiResourceCommand command)
        {
            var api = Mapper.Map<CreateOrUpdateApiResourceCommand, ApiResource>(command);
            if (string.IsNullOrEmpty(command.Id))
            {
                api.Id = Guid.NewGuid().ToString();
            }
            else
            {
                var exist = await _repository.GetById(api.Id);
                if (!exist.Enabled)
                {
                    throw new Exception("ApiResource can't be enabled with this command.");
                }
                api.Name = exist.Name;
            }
            return api;
        }
    }
}
