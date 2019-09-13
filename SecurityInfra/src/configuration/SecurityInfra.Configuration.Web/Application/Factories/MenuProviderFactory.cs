using AutoMapper;
using SecurityInfra.Configuration.MenuProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecurityInfra.Configuration.Web.Application.Commands;

namespace SecurityInfra.Configuration.Web.Application.Mappers
{
    public class MenuProviderFactory
    {
        private readonly IMenuProviderRepository _repository;
        public MenuProviderFactory(IMenuProviderRepository repository)
        {
            _repository = repository;
        }
        public async Task<MenuProvider> Map(CreateOrUpdateMenuProviderCommand command)
        {
            var m = Mapper.Map<CreateOrUpdateMenuProviderCommand, MenuProvider>(command);
            if (string.IsNullOrEmpty(command.Id))
            {
                m.Id = Guid.NewGuid().ToString();
            }
            else
            {
                var exist = await _repository.GetById(m.Id);
                if (!exist.Enabled)
                {
                    throw new Exception("Client can't be enabled with this command.");
                }
            }
            return m;
        }
    }
}
