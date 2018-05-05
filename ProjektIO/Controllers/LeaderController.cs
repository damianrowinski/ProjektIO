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
                if (group == null)
                {
                    return View("Error");
                }
                ViewModels viewModel = new ViewModels();
                viewModel.GroupView.Group = group;
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult EditStatute(ViewModels viewModel)
        {
            using (var db = new DatabaseContext())
            {
                KoloNaukowe dbEntry = db.KoloNaukowe.Find(viewModel.GroupView.Group.Id);
                if (ModelState.IsValid)
                {
                    dbEntry.Regulamin = viewModel.GroupView.Group.Regulamin;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(viewModel);
                }
            }
        }
    }
}