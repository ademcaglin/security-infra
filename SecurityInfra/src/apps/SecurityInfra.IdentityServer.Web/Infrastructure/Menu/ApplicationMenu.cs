using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.IdentityServer.Web.Infrastructure.Menu
{
    public class ApplicationMenu
    {
        public string Title { get; set; }

        public IList<ApplicationMenuItem> Menus { get; set; } = new List<ApplicationMenuItem>();
    }
}
