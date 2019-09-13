using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.EDevlet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Microsoft.AspNetCore.Authentication.EDevlet
{
    public class EDevletOptions : OAuthOptions
    {
        public EDevletOptions()
        {
            SignInScheme = EDevletDefaults.AuthenticationScheme;
            CallbackPath = new PathString("/signin-edevlet");
            //Scope.Add("Kimlik-Dogrula");
            //Scope.Add("Ad-Soyad");
        }
    }
}