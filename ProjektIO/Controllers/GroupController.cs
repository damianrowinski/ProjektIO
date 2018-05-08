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

        public ActionResult AddGroup()
        {
            ViewModels viewModel = new ViewModels();
            AddGroupViewModel addGroup = new AddGroupViewModel();
            KoloNaukowe group = new KoloNaukowe();
            addGroup.Group = group;
            viewModel.AddGroup = addGroup;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddGroup(ViewModels viewModel)
        {
            using (var db = new DatabaseContext())
            {
                Kategoria category = new Kategoria();
                category = db.Kategoria.FirstOrDefault(p => p.Nazwa == viewModel.AddGroup.Category);
                viewModel.AddGroup.Group.Aktywny = true;
                viewModel.AddGroup.Group.DataUtworzenia = DateTime.Today;
                viewModel.AddGroup.Group.DataDoUsuniecia = DateTime.Today.AddYears(1);
                viewModel.AddGroup.Group.KategoriaId = category.Id;
                return View();
            }
        }

        public ActionResult ShowGroup(int id)
        {
            using (var db = new DatabaseContext())
            {
                ViewModels viewModel = new ViewModels();
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
                ViewModels viewModel = new ViewModels();
                GroupListViewModel groupList = new GroupListViewModel();
                Kategoria category = db.Kategoria.FirstOrDefault(p => p.Nazwa == id);
                if (category == null)
                {
                    return View("Error", new string[] { "Nie ma kategorii o podanej nazwie" });
                }

                groupList.Groups = (db.KoloNaukowe.Where(p => p.KategoriaId == category.Id).
                 OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize)).ToList();
                if (groupList.Groups == null)
                {
                    return View("Error", new string[] { "Brak kół w kategorii" });
                }

                int totalItems = db.KoloNaukowe.Where(p => p.KategoriaId == category.Id).Count();
                groupList.Pages = (int)Math.Ceiling((decimal)totalItems / PageSize);
                groupList.CurrentPage = page;
                groupList.Category = id;
                groupList = SetDetails(groupList);

                viewModel.GroupList = groupList;
                return View(viewModel);
            }
        }

        public ActionResult ShowMembers(int id, int page = 1)
        {
            using (var db = new DatabaseContext())
            {
                ViewModels viewModel = new ViewModels();
                MembersViewModel members = new MembersViewModel();
                members.Members = db.Czlonkowie.Include("Uzytkownik").Where(p => p.IdKola == id)
                    .OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize).ToList();
                if (members ==  null)
                {
                    return View("Error", new string[] { "Brak członków" });
                }
                int totalItems = db.Czlonkowie.Where(p => p.IdKola == id).Count();
                members.Pages = (int)Math.Ceiling((decimal)totalItems / PageSize);
                members.CurrentPage = page;
                members.Group = db.KoloNaukowe.Find(id);

                viewModel.Group = members.Group;
                viewModel.Members = members;
                return View(viewModel);
            }
        }

        public ActionResult ContactAdmin(int id)
        {
            using (var db = new DatabaseContext())
            {
                ViewModels viewModel = new ViewModels();
                Czlonkowie member = new Czlonkowie();
                member = db.Czlonkowie.Include("Uzytkownik").FirstOrDefault(p => p.Rola == 1 && p.IdKola == id);
                if (member == null)
                {
                    return View("Error", new string[] { "Ta grupa nie ma przewodniczącego" });
                }

                viewModel.Group = member.KoloNaukowe;
                viewModel.Member = member;
                return View(viewModel);
            }
        }

        //jak nie bedzie chciało działać to zmienić nazwe z page na id
        public ActionResult Contact(int page = 1)
        {
            using (var db = new DatabaseContext())
            {
                ViewModels viewModel = new ViewModels();
                GroupListViewModel groupList = new GroupListViewModel();
                groupList.Groups = db.KoloNaukowe.Select(p => p).
                 OrderBy(p => p.Id).Skip((page - 1) * PageSize).Take(PageSize).ToList();
           
                if (groupList.Groups == null)
                {
                    return View("Error");
                }

                int totalItems = db.KoloNaukowe.Select(p => p).Count();
                groupList.Pages = (int)Math.Ceiling((decimal)totalItems / PageSize);
                groupList.CurrentPage = page;
                groupList = SetDetails(groupList);

                viewModel.GroupList = groupList;
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
                ViewModels viewModel = new ViewModels();

                viewModel.Group = portfolio.KoloNaukowe;
                viewModel.Portfolio = portfolio;
                return View(viewModel);
            }
        }

        public ActionResult ShowStatute(int id)
        {
            using (var db = new DatabaseContext())
            {
                ViewModels viewModel = new ViewModels();
                KoloNaukowe group = db.KoloNaukowe.Find(id);
                MembersViewModel members = new MembersViewModel();

                members.Group = group;

                if (group == null)
                {
                    return View("Error", new string[] { "Takie koło nie istnieje" });
                }
                viewModel.Group = members.Group;
                viewModel.Members = members;
                return View(viewModel);
            }
        }

        //przekazuje id koła
        public ActionResult ShowPosts(int id, int page = 1)
        {
            using (var db = new DatabaseContext())
            {
                ViewModels viewModel = new ViewModels();
                PostListViewModel postList = new PostListViewModel();
                KoloNaukowe group = new KoloNaukowe();
                List<string> authors = new List<string>();
                postList.Posts = db.Post.Where(p => p.IdKola == id).ToList();
                group = db.KoloNaukowe.FirstOrDefault(p => p.Id == id);
                if (postList.Posts == null)
                {
                    return View("Error", new string[] { "Brak postów" });
                }

                viewModel.PostList = postList;
                int totalItems = db.Post.Where(p => p.IdKola == id).Count();
                viewModel.PostList.Pages = (int)Math.Ceiling((decimal)totalItems / PageSize);
                viewModel.PostList.CurrentPage = page;

                foreach (Post post in viewModel.PostList.Posts)
                {
                    Czlonkowie member = db.Czlonkowie.Include("Uzytkownik").FirstOrDefault(p => p.Id == post.IdCzlonka);
                    if (member == null)
                    {
                        return View("Error");
                    }
                    string author = member.Uzytkownik.Imie + " " + member.Uzytkownik.Nazwisko;
                    authors.Add(author);
                }
                viewModel.PostList.AuthorsNames = authors;
                viewModel.PostList.Group = group;
                return View(viewModel);
            }
        }



        private GroupListViewModel SetDetails(GroupListViewModel model)
        {
            using (var db = new DatabaseContext())
            {
                foreach (KoloNaukowe group in model.Groups)
                {
                    GroupViewModel temp = new GroupViewModel();
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
                ViewModels viewModel = new ViewModels();
                Czlonkowie tempCzlonek = new Czlonkowie();
                Uzytkownik currentUser = User.GetUserData();
                Uzytkownik tempUser = db.Uzytkownik.Find(currentUser.Id);
                KoloNaukowe tempKolo = db.KoloNaukowe.Find(id);

                tempCzlonek.IdKola = tempKolo.Id;
                tempCzlonek.IdUzytkownika = tempUser.Id;
                tempCzlonek.Uzytkownik = tempUser;
                tempCzlonek.KoloNaukowe = tempKolo;
                tempCzlonek.Rola = 0;
                tempCzlonek.Aktywny = false;

                if (db.Czlonkowie.Any(p => p.IdUzytkownika == tempUser.Id && p.IdKola == tempKolo.Id))
                {
                    viewModel.Group = tempCzlonek.KoloNaukowe;
                    return View("JoinGroupError", viewModel);
                }

                db.Czlonkowie.Add(tempCzlonek);
                db.SaveChanges();

                viewModel.Group = tempCzlonek.KoloNaukowe;
                viewModel.Member = tempCzlonek;
                return View(viewModel);
            }
        }
    }
}