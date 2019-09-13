using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace FakeAuthentication
{
    public class FakeAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        public IList<Claim> Claims { get; set; } = new List<Claim>();
    }
}
