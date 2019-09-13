using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Authentication.EDevlet
{
    public static class EDevletDefaults
    {
        public const string AuthenticationScheme = "EDevlet";

        public const string LoginMethodClaimName = "EDevletLoginMethod";

        public const string AccessTokenPropName = "EDevletAccessToken";

        public const string DisplayName = "EDevletAuth";
    }
}