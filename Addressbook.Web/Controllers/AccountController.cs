using Addressbook.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Addressbook.Web.Controllers
{
    public class AccountController : Controller
    {
        public IAuthenticationManager  Authentication => HttpContext.GetOwinContext().Authentication; //new CSharp sintax.
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            //TODO: Perform Validation
            var isValid = true;

            //Sign User in
            if (isValid)
            {
                SignIn(model);
                return RedirectToAction("Index", "Home");
                //Redirect to HomeController
                //if (!string.IsNullOrEmpty(returnUrl))
                //{
                //    return Redirect(returnUrl);
                //}
                //else
                //{
                //    return RedirectToAction("Index", "Home");
                //}
            }
            else
            {
                return View(model);
            }
        }

        private void SignIn(LoginModel model)
        {
            var claims = new Claim[]{
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Name, model.Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            Authentication.SignIn(identity);
        }

        public ActionResult LogOut()
        {
            Authentication.SignOut();
            return Redirect("login");
        }
    }
}