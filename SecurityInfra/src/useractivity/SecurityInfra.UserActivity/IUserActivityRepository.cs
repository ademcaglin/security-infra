using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecurityInfra.UserActivity
{
    public interface IUserActivityRepository
    {
        IReadOnlyCollection<UserActivity> GetAllByUserId(string id);

        Task Add(UserActivity activity);
    }
}
