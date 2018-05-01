using System.Linq;

using ProjektIO.Models;
using ProjektIO.Libraries;
using System.Web.Security;
using System;
using System.Web;

namespace ProjektIO.Auth
{
    public static class Repository
    {
        public static Uzytkownik GetUserDetails(Uzytkownik user)
        {
            using (var db = new DatabaseContext())
            {
                var dbUser = db.Uzytkownik.FirstOrDefault(t => t.Login == user.Login);
                if (dbUser != default(Uzytkownik))
                {
                    string hashedPassword = StringLibrary.CreateMD5(user.Haslo + StringLibrary.CreateMD5(dbUser.Salt));
                    if (dbUser.Haslo == hashedPassword)
                    {
                        return dbUser;
                    }
                }
                return null;
            }
        }

        public static void LogIn(HttpResponseBase responseBase, Uzytkownik user)
        {
            FormsAuthentication.SetAuthCookie(user.Login, false);

            var authTicket = new FormsAuthenticationTicket(1, user.Login, DateTime.Now, DateTime.Now.AddMinutes(20), false, $"{user.Id}");
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            responseBase.Cookies.Add(authCookie);
        }
        
    }
}