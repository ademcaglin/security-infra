using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace FakeAuthentication
{
    public class FakeAuthenticationScheme
    {
        public string SchemeName { get; set; }

        public IList<Claim> Claims { get; set; } = new List<Claim>();
    }
}
