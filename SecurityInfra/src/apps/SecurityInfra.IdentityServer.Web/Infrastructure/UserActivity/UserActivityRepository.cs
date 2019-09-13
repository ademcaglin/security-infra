using Microsoft.Extensions.Logging;
using SecurityInfra.UserActivity;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.IdentityServer.Web.Infrastructure.UserActivity
{
    public class UserActivityRepository : IUserActivityRepository
    {
        public Task Add(SecurityInfra.UserActivity.UserActivity activity)
        {
            Log.Logger.ForContext("UserId", activity.UserId)
              .ForContext("ActionName", activity.ActionName)
              .ForContext("ActionName", activity.ActionName)
              .ForContext("ActionName", activity.ActionName)
              .ForContext("ActionName", activity.ActionName)
              .ForContext("ActionName", activity.ActionName)
              .ForContext("ActionName", activity.ActionName)
              .ForContext("Properties", activity.Properties)
              .Information(activity.Message);
            return Task.CompletedTask;
        }

        public IReadOnlyCollection<SecurityInfra.UserActivity.UserActivity> GetAllByUserId(string id)
        {
            throw new Exception("");
        }
    }
}
