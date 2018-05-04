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
    }
}