using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjektIO.Models;

namespace ProjektIO.Controllers
{
    public class LeaderController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        // GET: Leader
        //id w parametrze to id koła
        public ActionResult EditStatute(int id)
        {
            using (var db = new DatabaseContext())
            {
                KoloNaukowe group = new KoloNaukowe();
                if (group == null)
                {
                    return View("Error");
                }
                group = db.KoloNaukowe.FirstOrDefault(p => p.Id == id);
                return View(group);
            }
        }

        [HttpPost]
        public ActionResult EditStatute(KoloNaukowe group)
        {
            using (var db = new DatabaseContext())
            {
                KoloNaukowe dbEntry = db.KoloNaukowe.Find(group.Id);
                if (ModelState.IsValid)
                {
                    dbEntry.Regulamin = group.Regulamin;
                    db.SaveChanges();
                    TempData["message"] = string.Format("Zmiany zostały zapisane");
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(group.Regulamin);
                }
            }
        }
    }
}