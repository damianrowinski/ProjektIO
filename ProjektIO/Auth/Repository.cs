using System.Linq;

using ProjektIO.Models;
using ProjektIO.Libraries;

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

        
    }
}