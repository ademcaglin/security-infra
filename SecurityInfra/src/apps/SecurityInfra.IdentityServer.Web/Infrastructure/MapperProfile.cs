using AutoMapper;
using SecurityInfra.Configuration;
using SecurityInfra.Configuration.ApiResources;
using SecurityInfra.Configuration.Clients;
using SecurityInfra.Configuration.IdentityResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.IdentityServer.Web.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<IdentityServer4.Models.Client, Client>().ReverseMap();
            CreateMap<IdentityServer4.Models.IdentityResource, IdentityResource>().ReverseMap();
            CreateMap<IdentityServer4.Models.ApiResource, ApiResource>().ReverseMap();
            CreateMap<IdentityServer4.Models.Scope, Scope>().ReverseMap();
            CreateMap<IdentityServer4.Models.Secret, Secret>().ReverseMap();
        }
    }
}
