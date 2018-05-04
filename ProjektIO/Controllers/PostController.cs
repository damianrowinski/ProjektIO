using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjektIO.Models;

namespace ProjektIO.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        //przekazuje id członka
        public ActionResult AddPost(int id)
        {
            using (var db = new DatabaseContext())
            {
                PostViewModel viewModel = new PostViewModel();
                Czlonkowie member = db.Czlonkowie.Find(id);
                viewModel.Member = member;
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult AddPost(PostViewModel viewModel)
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
    }
}