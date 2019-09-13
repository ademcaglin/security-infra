using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInfra.Configuration.MenuProviders
{
    public interface IMenuProviderRepository
    {
        Task<IList<MenuProvider>> GetAll();

        Task<MenuProvider> GetById(string id);

        Task Save(MenuProvider provider);
    }
}
