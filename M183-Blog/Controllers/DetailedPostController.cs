using M183_Blog.Models;
using M183_Blog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace M183_Blog.Controllers
{
    public class DetailedPostController : Controller
    {
        public DataContext database = new DataContext();

        public ActionResult Index(int postId)
        {
            return View("Index", new DetailedPostViewModel(database.Posts.FirstOrDefault(p => p.Id == postId)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(int postId, string comment)
        {
            database.Posts.FirstOrDefault(p => p.Id == postId).Comment.Add(new Comment() {Commet = comment,PostId = postId,UserId = 1});
            
            return View("Index", new DetailedPostViewModel(database.Posts.FirstOrDefault(p => p.Id == postId)));
        }
    }
}