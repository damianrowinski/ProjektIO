using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProjektIO.Libraries;
using ProjektIO.Models;

namespace ProjektIO.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(Models.Account.LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Uzytkownik user = new Uzytkownik() { Login = model.Login, Haslo = model.Password };

            user = Auth.Repository.GetUserDetails(user);

            if (user != null && user != default(Uzytkownik))
            {
                FormsAuthentication.SetAuthCookie(model.Login, false);

                var authTicket = new FormsAuthenticationTicket(1, user.Login, DateTime.Now, DateTime.Now.AddMinutes(20), false, "");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                return RedirectToAction("Index", "Home");

            }

            else
            {
                ModelState.AddModelError("", "Podano niepoprawne dane");
                return View(model);
            }
        }
    }
}