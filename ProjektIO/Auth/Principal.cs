using System.Linq;
using System.Security.Principal;
using ProjektIO.Models;

namespace ProjektIO.Auth
{
    public class Principal : IPrincipal
    {

        public Uzytkownik userData { get; set; }

        public IIdentity Identity
        {
            get; private set;
        }

        public Principal(string username)
        {
            Identity = new GenericIdentity(username);
        }

        public bool IsInRole(string role)
        {
            throw new System.NotImplementedException();
        }
    }
}