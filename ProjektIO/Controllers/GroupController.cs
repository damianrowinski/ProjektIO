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
                if (group == null)
                {
                    return View("Error", new string[] { "Takie koło nie istnieje" });
                }
                return View(group);
            }
        }

        public ActionResult Categories(string id, int page = 1)
        {
            using (var db = new DatabaseContext())
            {
                GroupsListViewModels viewModel = new GroupsListViewModels();

                Kategoria category = db.Kategoria.FirstOrDefault(p => p.Nazwa == id);

                if (category == null)
                {
                    return View("Error", new string[] { "Nie ma kategorii o podanej nazwie" });
                }

                viewModel.Groups = (db.KoloNaukowe.Where(p => p.KategoriaId == category.Id).
                 OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize)).ToList();


                int totalItems = db.KoloNaukowe.Where(p => p.KategoriaId == category.Id).Count();
                viewModel.Pages = (int)Math.Ceiling((decimal)totalItems / PageSize);
                viewModel.CurrentPage = page;
                viewModel.Category = id;
                viewModel.SetDetails();

                return View(viewModel);
            }
        }

        public ActionResult ShowMembers(int id, int page = 1)
        {
            using (var db = new DatabaseContext())
            {
                MembersViewModels viewModel = new MembersViewModels();
                viewModel.Members = db.Czlonkowie.Include("Uzytkownik").Where(p => p.IdKola == id)
                    .OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize).ToList();
                int totalItems = db.Czlonkowie.Where(p => p.IdKola == id).Count();
                viewModel.Pages = (int)Math.Ceiling((decimal)totalItems / PageSize);
                viewModel.CurrentPage = page;
                viewModel.GroupId = id;

                return View(viewModel);
            }
        }

        public ActionResult ContactAdmin(int id)
        {
            using (var db = new DatabaseContext())
            {
                Czlonkowie admin = new Czlonkowie();
                admin = db.Czlonkowie.Include("Uzytkownik").FirstOrDefault(p => p.Rola == 1 && p.IdKola == id);
                return View(admin);
            }
        }

        //jak nie bedzie chciało działać to zmienić nazwe z page na id
        public ActionResult Contact(int page = 1)
        {
            using (var db = new DatabaseContext())
            {
                GroupsListViewModels viewModel = new GroupsListViewModels();


                viewModel.Groups = db.KoloNaukowe.Select(p => p).
                 OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize).ToList();


                int totalItems = db.KoloNaukowe.Select(p => p).Count();
                viewModel.Pages = (int)Math.Ceiling((decimal)totalItems / PageSize);
                viewModel.CurrentPage = page;
                viewModel.SetDetails();

                return View(viewModel);
            }
        }

        public ActionResult ShowPortfolio(int id)
        {
            using (var db = new DatabaseContext())
            {
                Portfolio portfolio = db.Portfolio.Include("KoloNaukowe").FirstOrDefault(p => p.IdKola == id);

                if (portfolio == null)
                {
                    return View("Error", new string[] { "Nie znaleziono portfolio dla kola" });
                }

                return View(portfolio);
            }
        }
    }
}