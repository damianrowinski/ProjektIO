using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektIO.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Statute()
        {
            return View();
        }
        public ActionResult News()
        {
            return View();
        }
        public ActionResult ContactToGroups()
        {
            return View();
        }
        public ActionResult Group()
        {
            return View("~/Views/Group/Group.cshtml");
        }
        public ActionResult GroupList()
        {
            return View("~/Views/Group/GroupList.cshtml");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}