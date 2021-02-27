using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Auto_Invitation.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpPost("~/signin")]
        public async Task<IActionResult> SignIn()
        {
            return Challenge(new AuthenticationProperties {RedirectUri = "/"}, "GitHub");
        }

        [HttpGet("~/signout")]
        [HttpPost("~/signout")]
        public IActionResult SignOutCurrentUser()
        {
            return SignOut(new AuthenticationProperties {RedirectUri = "/"},
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}