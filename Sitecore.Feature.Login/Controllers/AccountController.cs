using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Security.AccessControl;
using Sitecore.Security.Authentication;

namespace Sitecore.Feature.Login.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            string fullUsername = $"sitecore\\{username}"; // Ensure domain prefix, Sitecore or Extranet

            if (AuthenticationManager.Login(fullUsername, password))
            {
                // Redirect on success
                string homeUrl = Sitecore.Links.LinkManager.GetItemUrl(Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath));
                return Redirect(homeUrl);
            }

            ViewBag.ErrorMessage = "Invalid username or password";
            return View();
        }

        public ActionResult Logout()
        {
            AuthenticationManager.Logout();
            return RedirectToAction("Login");
        }
    }

}