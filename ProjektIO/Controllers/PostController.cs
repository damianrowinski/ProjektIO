using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjektIO.Models;
using Microsoft.AspNet.Identity;

namespace ProjektIO.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult AddPost()
        {
            using (var db = new DatabaseContext())
            {
                AddPostViewModels viewModel = new AddPostViewModels();
                int userId = (User as ProjektIO.Auth.Principal).GetUserData().Id;
                Czlonkowie member = db.Czlonkowie.FirstOrDefault(p => p.IdUzytkownika == userId);
                viewModel.Member = member;
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult AddPost(AddPostViewModels viewModel)
        {
            using (var db = new DatabaseContext())
            {
                Post post = new Post();
                post.IdCzlonka = viewModel.Member.Id;
                post.Przypiety = false;
                post.DataUtworzenia = DateTime.Now;
                post.Zawartosc = viewModel.PostContent;
                post.IdKola = viewModel.Member.IdKola;
                post.KoloNaukowe = db.KoloNaukowe.Find(viewModel.Member.IdKola);
                post.Czlonkowie = db.Czlonkowie.Find(viewModel.Member.Id);
                db.Post.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        
        //id to id postu
        public ActionResult AddComment(int id)
        {
           AddCommentViewModels viewModel = new AddCommentViewModels();
            viewModel.PostId = id;
           return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddComment (AddCommentViewModels viewModel)
        {
            using (var db = new DatabaseContext())
            {
                int id = (User as ProjektIO.Auth.Principal).GetUserData().Id;
                Czlonkowie member = db.Czlonkowie.FirstOrDefault(p => p.IdUzytkownika == id);
                Post post = db.Post.Find(viewModel.PostId);
                if (member == null || post == null)
                {
                    return View("Error");
                }
                viewModel.Comment.IdPostu = post.Id;
                viewModel.Comment.Post = post;
                viewModel.Comment.IdCzlonka = member.Id;
                viewModel.Comment.Czlonkowie = member;
                db.Komentarz.Add(viewModel.Comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //przekazuje id posta
        public ActionResult ShowPost(int id, int page = 1)
        {
            int PageSize = 5;

            using (var db = new DatabaseContext())
            {
                PostViewModels viewModel = new PostViewModels();
                viewModel.Post = db.Post.Find(id);
                viewModel.Comments = db.Komentarz.Where(p => p.IdPostu == id).ToList();
                Czlonkowie author = db.Czlonkowie.Include("Uzytkownik").FirstOrDefault(p => p.Id == viewModel.Post.IdCzlonka);
                viewModel.AuthorName = author.Uzytkownik.Imie + " " + author.Uzytkownik.Nazwisko;
                if (viewModel.Post == null || author == null)
                {
                    return View("Error");
                }
                int totalItems = db.Komentarz.Where(p => p.IdPostu == id).ToList().Count();
                viewModel.Pages = (int)Math.Ceiling((decimal)totalItems / PageSize);
                viewModel.CurrentPage = page;
                viewModel = SetCommentsAuthors(viewModel);
                return View(viewModel);
            }
        }

        private PostViewModels SetCommentsAuthors (PostViewModels postModel)
        {
            using (var db = new DatabaseContext())
            {
                foreach (Komentarz comment in postModel.Comments)
                {
                    string temp;
                    Czlonkowie author = db.Czlonkowie.Include("Uzytkownik").FirstOrDefault(p => p.Id == comment.IdCzlonka);
                    temp = author.Uzytkownik.Imie + " " + author.Uzytkownik.Nazwisko;
                    postModel.CommentsAuthors.Add(temp);
                }
                return postModel;
            }
        }
    }
}