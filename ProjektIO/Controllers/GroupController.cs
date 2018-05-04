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
                MembersViewModels viewModel = new MembersViewModels();
                KoloNaukowe group = db.KoloNaukowe.Find(id);
                viewModel.Group = group;
                if (group == null)
                {
                    return View("Error", new string[] { "Takie koło nie istnieje" });
                }
                return View(viewModel);
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
                if (viewModel.Groups == null)
                {
                    return View("Error", new string[] { "Brak kół w kategorii" });
                }

                int totalItems = db.KoloNaukowe.Where(p => p.KategoriaId == category.Id).Count();
                viewModel.Pages = (int)Math.Ceiling((decimal)totalItems / PageSize);
                viewModel.CurrentPage = page;
                viewModel.Category = id;
                viewModel = SetDetails(viewModel);
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
                if (viewModel.Members ==  null)
                {
                    return View("Error", new string[] { "Brak członków" });
                }
                int totalItems = db.Czlonkowie.Where(p => p.IdKola == id).Count();
                viewModel.Pages = (int)Math.Ceiling((decimal)totalItems / PageSize);
                viewModel.CurrentPage = page;
                viewModel.Group = db.KoloNaukowe.Find(id);

                return View(viewModel);
            }
        }

        public ActionResult ContactAdmin(int id)
        {
            using (var db = new DatabaseContext())
            {
                Czlonkowie leader = new Czlonkowie();
                leader = db.Czlonkowie.Include("Uzytkownik").FirstOrDefault(p => p.Rola == 1 && p.IdKola == id);
                if (leader == null)
                {
                    return View("Error", new string[] { "Ta grupa nie ma przewodniczącego" });
                }
                return View(leader);
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

                if (viewModel.Groups == null)
                {
                    return View("Error");
                }

                int totalItems = db.KoloNaukowe.Select(p => p).Count();
                viewModel.Pages = (int)Math.Ceiling((decimal)totalItems / PageSize);
                viewModel.CurrentPage = page;
                viewModel = SetDetails(viewModel);

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

        public ActionResult ShowStatute(int id)
        {
            using (var db = new DatabaseContext())
            {
                MembersViewModels viewModel = new MembersViewModels();
                KoloNaukowe group = db.KoloNaukowe.Find(id);

                viewModel.Group = group;

                if (group == null)
                {
                    return View("Error", new string[] { "Takie koło nie istnieje" });
                }
                return View(viewModel);
            }
        }
        
        public GroupsListViewModels SetDetails(GroupsListViewModels model)
        {
            using (var db = new DatabaseContext())
            {
                foreach (KoloNaukowe group in model.Groups)
                {
                    GroupViewModels temp = new GroupViewModels();
                    Czlonkowie leader = new Czlonkowie();
                    leader = db.Czlonkowie.Include("Uzytkownik").First(p => p.IdKola == group.Id && p.Rola == 1);
                    if (leader != null)
                    {
                        temp.LeaderName = leader.Uzytkownik.Imie + " " + leader.Uzytkownik.Nazwisko ?? "Brak";
                        temp.Mail = leader.Uzytkownik.Email ?? "Brak";
                        temp.Group = group;
                        temp.PhoneNumber = leader.Uzytkownik.NumerTelefonu ?? "Brak";
                    }
                    model.viewGroup.Add(temp);
                }
                return model;
            }
        }
        public ActionResult JoinGroup(int id)
        {
            using (var db = new DatabaseContext())
            {
                Czlonkowie tempCzlonek = new Czlonkowie();
                Uzytkownik currentUser = User.GetUserData();
                Uzytkownik tempUser = db.Uzytkownik.Find(currentUser.Id);
                KoloNaukowe tempKolo = db.KoloNaukowe.Find(id);
                bool v;

                tempCzlonek.IdKola = tempKolo.Id;
                tempCzlonek.IdUzytkownika = tempUser.Id;
                tempCzlonek.Uzytkownik = tempUser;
                tempCzlonek.KoloNaukowe = tempKolo;
                tempCzlonek.Rola = 0;
                tempCzlonek.Aktywny = false;

                if (db.Czlonkowie.Any(p => p.IdUzytkownika == tempUser.Id && p.IdKola == tempKolo.Id))
                {
                    //mozna tutaj podac np error
                    v = false;
                    return View("JoinGroup",v);
                }

                db.Czlonkowie.Add(tempCzlonek);
                db.SaveChanges();

                v = true;
                
                return View("JoinGroup",v);
            }
        }
    }
}