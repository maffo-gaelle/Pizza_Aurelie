using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Pizza_Aurelie.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public bool DisplayInvalidAccountMessage = false;
        IConfiguration _configuration;
        //Le configuration ci, je le recupère de configuration du constructeur classe Startup qui lui vient du AppSettingJson
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnGet()
        {
            if(HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Admin/Pizzas");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string username, string password, string ReturnUrl)
        {
            IConfigurationSection authSection = _configuration.GetSection("Auth");
            string adminLogin = authSection["AdminLogin"];
            string adminPassword = authSection["AdminPassword"];
            
            if (username == adminLogin && password == adminPassword)
            {
                DisplayInvalidAccountMessage = false;
                //Le claims ici va nous permmetre nous permettre de dire à l'authentication que on peut authorizer l'accès
                //puisque cette personne admin est connu
                var claims = new List<Claim>
                {
                    //Le claims nous dit que ce user est valide
                    new Claim(ClaimTypes.Name, username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new
                ClaimsPrincipal(claimsIdentity));
                //Dans le cas où l'authentication réussi, si on a pas d'url, on dirige vers liste de pizzas si oui, on dirige vers l'url en question
                return Redirect(ReturnUrl == null ? "/Admin/Pizzas" : ReturnUrl);
            }

            DisplayInvalidAccountMessage = true;
            return Page();
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
           
            return Redirect("/Admin");
        }
    }
}
