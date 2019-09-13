using System;
using System.Collections.Generic;
using System.Text;

namespace FakeAuthentication
{
    public class FakeAuthenticationOptions
    {
        public IList<FakeAuthenticationScheme> Schemes { get; set; } = new List<FakeAuthenticationScheme>();
    }
}
