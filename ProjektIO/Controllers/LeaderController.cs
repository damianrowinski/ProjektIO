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
                group = db.KoloNaukowe.FirstOrDefault(p => p.Id == id);
                string statute = group.Regulamin;
                return View(statute);
            }
        }

        [HttpPost]
        public ActionResult EditStatute(int id, string statute)
        {
            using (var db = new DatabaseContext())
            {
                KoloNaukowe group = new KoloNaukowe();
                group = db.KoloNaukowe.FirstOrDefault(p => p.Id == id);
                if (ModelState.IsValid)
                {
                    group.Regulamin = statute;
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