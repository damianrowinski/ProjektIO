using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjektIO.Models;
using Microsoft.AspNet.Identity;
using ProjektIO.Libraries;


//trzeba bedzie pozmieniac wszystkie redirect to action index
namespace ProjektIO.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult AddPost()
        {
            using (var db = new DatabaseContext())
            {
                ViewModels viewModel = new ViewModels();
                AddPostViewModel addPost = new AddPostViewModel();
                int userId = (User as ProjektIO.Auth.Principal).GetUserData().Id;
                Czlonkowie member = db.Czlonkowie.FirstOrDefault(p => p.IdUzytkownika == userId);
                addPost.Member = member;
                viewModel.AddPost = addPost;
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult AddPost(ViewModels viewModel)
        {
            using (var db = new DatabaseContext())
            {
                Post post = new Post();
                post.IdCzlonka = viewModel.AddPost.Member.Id;
                post.Przypiety = false;
                post.DataUtworzenia = DateTime.Now;
                post.Zawartosc = viewModel.AddPost.PostContent;
                post.IdKola = viewModel.AddPost.Member.IdKola;
                post.KoloNaukowe = db.KoloNaukowe.FirstOrDefault(p => p.Id == viewModel.AddPost.Member.IdKola);
                post.Czlonkowie = db.Czlonkowie.FirstOrDefault(p => p.Id == viewModel.AddPost.Member.Id);
                post.AktywneKom = true;
                db.Post.Add(post);
                db.SaveChanges();
                return View();
            }
        }


        //id to id postu
        [Authorize(Roles = RoleLibrary.ADMIN + "," + RoleLibrary.MEMBER)]
        public ActionResult AddComment(int id)
        {
            ViewModels viewModel = new ViewModels();
            AddCommentViewModel addComment = new AddCommentViewModel();
            viewModel.AddComment = addComment;
            viewModel.AddComment.PostId = id;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddComment (ViewModels viewModel)
        {
            using (var db = new DatabaseContext())
            {
                int id = (User as ProjektIO.Auth.Principal).GetUserData().Id;
                Czlonkowie member = db.Czlonkowie.FirstOrDefault(p => p.IdUzytkownika == id);
                Post post = db.Post.FirstOrDefault(p => p.Id == viewModel.AddComment.PostId);
                if (member == null || post == null)
                {
                    return View("Error");
                }
                viewModel.AddComment.Comment.IdPostu = post.Id;
                viewModel.AddComment.Comment.Post = post;
                viewModel.AddComment.Comment.IdCzlonka = member.Id;
                viewModel.AddComment.Comment.Czlonkowie = member;
                db.Komentarz.Add(viewModel.AddComment.Comment);
                db.SaveChanges();
                return View();
            }
        }

        //przekazuje id posta
       [Authorize(Roles = RoleLibrary.ADMIN + "," + RoleLibrary.MEMBER)]
        public ActionResult ShowPost(int id, int page = 1)
        {
            int PageSize = 5;

            using (var db = new DatabaseContext())
            {
                PostViewModel post = new PostViewModel();
                ViewModels viewModel = new ViewModels();
                post.Post = db.Post.FirstOrDefault(p => p.Id == id);
                post.Comments = db.Komentarz.Where(p => p.IdPostu == id).ToList();
                Czlonkowie author = db.Czlonkowie.Include("Uzytkownik").FirstOrDefault(p => p.Id == post.Post.IdCzlonka);
                post.AuthorName = author.Uzytkownik.Imie + " " + author.Uzytkownik.Nazwisko;
                if (post.Post == null || author == null)
                {
                    return View("Error");
                }
                int totalItems = db.Komentarz.Where(p => p.IdPostu == id).ToList().Count();
                post.Pages = (int)Math.Ceiling((decimal)totalItems / PageSize);
                post.CurrentPage = page;
                post = SetCommentsAuthors(post);
                viewModel.PostModel = post;
                return View(viewModel);
            }
        }

        //przekazuje id postu
        [Authorize(Roles = RoleLibrary.POST_AUTHOR)]
        public ActionResult EditPost(int id)
        {
            using (var db = new DatabaseContext())
            {
                Post post = new Post();
                post = db.Post.FirstOrDefault(p => p.Id == id);
                if (post == null)
                {
                    return View("Error");
                }
                ViewModels viewModel = new ViewModels();
                viewModel.Post = post;
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult EditPost (ViewModels viewModel)
        {
            using (var db = new DatabaseContext())
            {
                Post dbEntry = db.Post.FirstOrDefault(p => p.Id == viewModel.Post.Id);
                if (ModelState.IsValid)
                {
                    dbEntry.Zawartosc = viewModel.Post.Zawartosc;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(viewModel);
                }
            }
        }

        //przekazuje id postu
        [Authorize(Roles = RoleLibrary.ADMIN + "," + RoleLibrary.POST_AUTHOR)]
        public ActionResult DeletePost(int id)
        {
            using (var db = new DatabaseContext())
            {
                Post dbEntry = new Post();
                List<Komentarz> comEntry = new List<Komentarz>();
                dbEntry = db.Post.FirstOrDefault(p => p.Id == id);
                comEntry = db.Komentarz.Where(p => p.IdPostu == id).ToList();
                if (dbEntry == null)
                {
                    return View("Error");
                }
                db.Post.Remove(dbEntry);
                foreach (Komentarz comment in comEntry)
                {
                    db.Komentarz.Remove(comment);
                }
                db.SaveChanges();
                return View();
            }
        }

        //przekazuje id komentarza
        [Authorize(Roles = RoleLibrary.COMMENT_AUTHOR)]
        public ActionResult EditComment(int id)
        {
            using (var db = new DatabaseContext())
            {
                Komentarz comment = new Komentarz();
                comment = db.Komentarz.FirstOrDefault(p => p.Id == id);
                if (comment == null)
                {
                    return View("Error");
                }
                ViewModels viewModel = new ViewModels();
                viewModel.Comment = comment;
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult EditComment(ViewModels viewModel)
        {
            using (var db = new DatabaseContext())
            {
                Komentarz dbEntry = db.Komentarz.FirstOrDefault(p => p.Id == viewModel.Comment.Id);
                if (ModelState.IsValid)
                {
                    dbEntry.Zawartosc = viewModel.Comment.Zawartosc;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(viewModel);
                }
            }
        }

        //przekazuje id komentarza
        [Authorize(Roles = RoleLibrary.ADMIN + "," + RoleLibrary.COMMENT_AUTHOR)]
        public ActionResult DeleteComment(int id)
        {
            using (var db = new DatabaseContext())
            {
                Komentarz dbEntry = new Komentarz();
                dbEntry = db.Komentarz.FirstOrDefault(p => p.Id == id);
                if (dbEntry == null)
                {
                    return View("Error");
                }
                db.Komentarz.Remove(dbEntry);
                db.SaveChanges();
                return View();
            }
        }

       private PostViewModel SetCommentsAuthors (PostViewModel postModel)
        {
            using (var db = new DatabaseContext())
            {
                List<string> authors = new List<string>();
                foreach (Komentarz comment in postModel.Comments)
                {
                    string temp;
                    Czlonkowie author = db.Czlonkowie.Include("Uzytkownik").FirstOrDefault(p => p.Id == comment.IdCzlonka);
                    temp = author.Uzytkownik.Imie + " " + author.Uzytkownik.Nazwisko;
                    authors.Add(temp);
                }
                postModel.CommentsAuthors = authors;
                return postModel;
            }
        }
    }
}