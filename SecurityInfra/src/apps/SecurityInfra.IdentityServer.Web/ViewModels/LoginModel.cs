using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityInfra.IdentityServer.Web.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Kullanıcı adı giriniz")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Şifre giriniz")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public bool IsLocalContext { get; set; }
    }
}
