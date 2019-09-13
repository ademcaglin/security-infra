﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.Web.Application.Queries.Clients
{
    public interface IClientQueryService
    {
        Task<ClientPaginatedList> GetPaginatedList(ClientListQuery query);
    }
}
