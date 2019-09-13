using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecurityInfra.Identity.IdentityUsers;

namespace SecurityInfra.Management.Web.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class IdentityUsersController : Controller
    {
        private readonly IIdentityUserRepository _identityUserRepository;
        public IdentityUsersController(IIdentityUserRepository identityUserRepository)
        {
            _identityUserRepository = identityUserRepository;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _identityUserRepository.GetAll();
            // find current page 
            // if tenant admin get param
            // get all roles with current tenant
            // filter for rule

            // if resource admin get param
            // get all roles with current resource

            return new string[] { "value1", "value2" };
        }
        
        [HttpGet("{id}")]
        public string Get(int id)
        {
            // 
            return "value";
        }
        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("{id}/roles")]
        public string GetRoles(int id)
        {
            // get page header
            // if tenant then check user role, get tenant roles, filter with rule
            // if resource then check user role, get resource roles, filter with rule
            // send roles 
            return "value";
        }
    }
}
