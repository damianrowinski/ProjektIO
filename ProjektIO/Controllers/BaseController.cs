using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektIO.Controllers
{
    public class BaseController : Controller
    {
        protected virtual new Auth.Principal User
        {
            get { return HttpContext.User as Auth.Principal; }
        }
    }
}