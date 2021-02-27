using System;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

namespace Auto_Invitation.Web.Controllers
{
    public class ApiController : Controller
    {
        [HttpPost]
        [Route("/invite")]
        public IActionResult Invite(string email = "", string apikey = "")
        {
            if (email == "" && apikey == "")
            {
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