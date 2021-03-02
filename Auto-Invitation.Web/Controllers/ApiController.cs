using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using Octokit.Internal;

namespace Auto_Invitation.Web.Controllers
{
    public class ApiController : Controller
    {
        [HttpPost]
        [Route("~/invite")]
        public async Task<IActionResult> Invite(string email = "", string apikey = "")
        {
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(apikey))
            {
                if (User.Identity.IsAuthenticated)
                {
                    string accessToken = await HttpContext.GetTokenAsync("access_token");
                    var github = new GitHubClient(new ProductHeaderValue("AspNetCoreGitHubAuth"),
                        new InMemoryCredentialStore(new Credentials(accessToken)));
                    var l = await github.User.Email.GetAll();
                    if (l == null || l.Count < 1)
                    {
                        goto NoVerifiedEmail;
                    }

                    foreach (var item in l)
                    {
                        if (!item.Verified) continue;
                        email = item.Email;
                        goto AuthPassed;
                    }
                    NoVerifiedEmail:
                    ViewData["H1"] = "Sorry";
                    ViewData["Msg"] = "We cannot get an verified email from your GitHub account. :(";
                    return View();
                }

                ViewData["H1"] = "Welcome";
                ViewData["Msg"] = "You should add something. :D";
                return View();
            }

            if (!string.IsNullOrWhiteSpace(Shared.Config.Auth) && apikey != Shared.Config.Auth)
            {
                ViewData["H1"] = "Unauthorized";
                ViewData["Msg"] = "You don't have permission to do this! :(";
                return View();
            }

            AuthPassed:

            if (!MailAddress.TryCreate(email, out MailAddress ma))
            {
                ViewData["H1"] = "Bad Request";
                ViewData["Msg"] = "Oops! Maybe your mail address is invalid? :(";
                return View();
            }

            var m = GitHub.InviteToOrg(Shared.Config.Org, ma).Result;

            if (m.Status == HttpStatusCode.Created)
            {
                ViewData["H1"] = "OK";
                ViewData["Msg"] = "Thank you for support!\r\nAn mail with invitation will send to your mail soon! :D";
            }
            else
            {
                ViewData["H1"] = "Error";
                ViewData["Msg"] = "Oops! Something happened! :(";
            }

            return View();
        }
    }
}