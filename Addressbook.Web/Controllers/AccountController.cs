using Addressbook.Core.Interface.Managers;
using Addressbook.Core.Models;
using Addressbook.Web.Models;
using Addressbook.Web.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace Addressbook.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User, int> _user;

        // If also can define it and make it readonly, without a set: stackoverflow.com/questions/17881091/getter-and-setter-declaration-in-net
        //docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/using-properties
        public IAuthenticationManager  Authentication => HttpContext.GetOwinContext().Authentication; //new CSharp sintax.



        public AccountController(IAccountManager account)
        {
            _user = new UserManager<User, int>(new UserStore(account)); //v15 5.17
        }



        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            //validate user
            //sigin user
            var validateAndSigIn = from user in ValidateUser(model)
                                   from signIn in SignIn(user, model.RememberMe)
                                   select user;

            //TODO: Perform Validation
            var isValid = validateAndSigIn.Succeeded;

            //Sign User in
            if (isValid)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }

        private Operation<User> ValidateUser(LoginModel model) //v16 0.16
        {
            return Operation.Create(() =>
            {
                if (ModelState.IsValid)
                {
                    var user = _user.Find(model.Email, model.Password);
                    if (user == null)
                        throw new Exception("Invalid Username");
                    return user;
                }
                else
                {
                    var error = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .Aggregate((ag, e) => ag + ", " + e);

                    throw new Exception(error);
                }
            });
        }

        private Operation<ClaimsIdentity> SignIn(User model, bool rememberMe)
        {
            return Operation.Create(() =>
            {
                var identity = _user.CreateIdentity(model, DefaultAuthenticationTypes.ApplicationCookie);

                //Optionally add additiona claims

                Authentication.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, identity); //V15 9.24

                return identity;
            });
            
        }

        public ActionResult LogOut()
        {
            Authentication.SignOut();
            return Redirect("login");
        }
    }
}