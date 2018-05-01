using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjektIO.Models;

namespace ProjektIO.Controllers
{
    public class GroupController : BaseController
    {
        int PageSize = 2;
        // GET: Group
        public ActionResult ShowGroup(int id)
        {
            using (var db = new DatabaseContext())
            {
                KoloNaukowe group = db.KoloNaukowe.Find(id);
                return View(group);
            }
        }

        public ActionResult Categories(int id, int page = 1)
        {
            using (var db = new DatabaseContext())
            {
                GroupsViewModel viewModel = new GroupsViewModel();

                viewModel.Groups = db.KoloNaukowe.Where(p => p.Kategoria == id).
                 OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize).ToList();

                int totalItems = db.KoloNaukowe.Where(p => p.Kategoria == id).Count();
                viewModel.Pages = (int)Math.Ceiling((decimal)totalItems / PageSize);
                viewModel.CurrentPage = page;
                viewModel.Category = id;

                return View(viewModel);
            }
        }
    }
}