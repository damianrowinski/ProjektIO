using System;
using System.Linq;
using System.Security.Principal;
using ProjektIO.Models;

namespace ProjektIO.Auth
{
    public class Principal : IPrincipal
    {

        private Uzytkownik userData { get; set; }

        public IIdentity Identity
        {
            get; private set;
        }

        public Principal(string username)
        {
            Identity = new GenericIdentity(username);
            
        }

        public Principal(int userId)
        {
            using (var db = new DatabaseContext())
            {
                userData = db.Uzytkownik.FirstOrDefault(t => t.Id == userId);
                Identity = new GenericIdentity(userData.Login);
            }
        }

        public Uzytkownik GetUserData()
        {
            return userData;
        }

        public bool IsInRole(string role)
        {
            
            var x = new Random().NextDouble() > 0.5;
            return x;
        }
    }
}