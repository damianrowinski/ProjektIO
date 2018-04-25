using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProjektIO.Libraries;
using ProjektIO.Models;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace ProjektIO.Controllers
{
    public class UserController : Controller
    {
        
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

        public ActionResult LoginCAS()
        {
            string proxyKey = "05e9d57c-f2d2-4e6d-889a-b99bbf983570";
            string proxyRedirect = "http://localhost:50860/User/CASResponse/";
            string proxyUrl = HttpUtility.UrlEncode($"http://www.mpenar.kia.prz.edu.pl/casproxy.php?redirect={proxyRedirect}&key={proxyKey}");
            return Redirect($"https://cas.prz.edu.pl/cas-server/login?service={proxyUrl}");
        }

        public ActionResult CASResponse()
        {
            string response = Request.QueryString["response"];
            byte[] data = Convert.FromBase64String(response.Replace(' ', '+'));
            string decodedString = Regex.Replace(Encoding.UTF8.GetString(data), @"\t|\n|\r|:", "");

            StringBuilder output = new StringBuilder();
            // Create an XmlReader
            using (XmlReader reader = XmlReader.Create(new StringReader(decodedString)))
            {
                reader.ReadToFollowing("casuser");
                output.AppendLine($"Użytkownik: {reader.ReadElementContentAsString()}<br />");
                reader.ReadToFollowing("casuid");
                output.AppendLine($"CAS UID: {reader.ReadElementContentAsString()}<br />");
                reader.ReadToFollowing("casmail");
                output.AppendLine($"CAS MAIL: {reader.ReadElementContentAsString()}<br />");
                reader.ReadToFollowing("casusos_id");
                output.AppendLine($"USOS ID: {reader.ReadElementContentAsString()}<br />");
                reader.ReadToFollowing("casemployeetype");
                output.AppendLine($"Employee Type: {reader.ReadElementContentAsString()}<br />");
                reader.ReadToFollowing("casregisteredaddress");
                output.AppendLine($"REGISTERED ADDRESS: {reader.ReadElementContentAsString()}<br />");
                reader.ReadToFollowing("casdepartmentnumber");
                output.AppendLine($"Department Number: {reader.ReadElementContentAsString()}<br />");
                reader.ReadToFollowing("casgivenname");
                output.AppendLine($"Imię: {reader.ReadElementContentAsString()}<br />");
                reader.ReadToFollowing("cassn");
                output.AppendLine($"Nazwisko: {reader.ReadElementContentAsString()}<br />");
            }
            return Content(output.ToString());



            /*XmlSerializer ser = new XmlSerializer(typeof(Models.Account.casServiceResponse));
            Models.Account.casServiceResponse casResponse = (Models.Account.casServiceResponse)ser.Deserialize(new StringReader(decodedString));*/



            return Content(HttpUtility.HtmlEncode(decodedString));
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(Models.Account.RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new Models.DatabaseContext())
            {

                if (db.Uzytkownik.Any(t => t.Login == model.Login))
                {
                    ModelState.AddModelError("", $"Użytkownik o nazwie \"{model.Login}\" istnieje już w bazie danych.");
                    return View(model);
                }

                Uzytkownik user = new Uzytkownik();
                user.Login = model.Login;
                user.Salt = StringLibrary.RandomString(6);
                user.Haslo = StringLibrary.CreateMD5(model.Password + StringLibrary.CreateMD5(user.Salt));

                user.DataUtworzenia = DateTime.Now;
                user.DataModyfikacji = DateTime.Now;
                user.IDP = 1;
                user.Rola = 0;

                db.Uzytkownik.Add(user);
                db.SaveChanges();
            }
            return RedirectToAction("Login", "User");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}