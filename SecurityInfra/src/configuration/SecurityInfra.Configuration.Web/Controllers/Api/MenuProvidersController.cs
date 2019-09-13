using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SecurityInfra.Common.Cqrs;
using SecurityInfra.Configuration.Web.Application.Commands;
using SecurityInfra.Configuration.Web.Application.Queries.MenuProviders;

namespace SecurityInfra.Configuration.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class MenuProvidersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMenuProviderQueryService _queryService;
        public MenuProvidersController(IMediator mediator,
            IMenuProviderQueryService queryService)
        {
            _mediator = mediator;
            _queryService = queryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(MenuProviderListQuery query)
        {
            var r = await _queryService.GetPaginatedList(query);
            var list = r.Data;
            Response.Headers.Add("X-Pagination-TotalRecords", r.TotalRecords.ToString());
            return Ok(list);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateOrUpdateMenuProviderCommand command)
        {
            var r = await _mediator.Send(command);
            if (!r.IsSucceed)
            {
                return BadRequest();
            }
            return Ok();
        }

        // DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var r = await _mediator.Send(new DisableMenuProviderCommand() {  MenuProviderId = id });
        //    if (!r.IsSucceed)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok();
        //}
    }
}
