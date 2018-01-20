using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using M183_Blog.Helpers;
using M183_Blog.Models;
using M183_Blog.ViewModels;

namespace M183_Blog.Controllers
{
    public class HomeController : Controller
    {
        public DataContext database = new DataContext();

        private PostRepo postRepo;

        // GET: Home
        public ActionResult Index()
        {
            postRepo = new PostRepo(database);
            IEnumerable<Post> posts = postRepo.GetPublicPosts();
            return View(new HomeViewModel(posts));
        }

        public ActionResult Seed()
        {
            User myUser = new User
            {
                Username = "Minachi",
                Password = SecurePasswordHasher.Hash("1234"),
                Firstname = "Julian",
                Familyname = "Manickam",
                Mobilephonenumber = "078 242 22 23",
                Role = "1",
                Status = "single"
            };

            User user = new User()
            {
                Username = "Blue",
                Password = "1234",
                Firstname = "Jonas",
                Familyname = "Wyss",
                Mobilephonenumber = "0753633622",
                Role = "1",
                Status = "single"
            };

            Comment comment = new Comment
            {
                Commet = "test comment",
                CreatedOn = DateTime.Today,
                User = myUser
            };

            Post publicPost = new Post()
            {
                Content = "beispiel text",
                CreatedOn = DateTime.Now,
                Description = "Beispielbeschreibung",
                Status = PostStatus.Public,
                Title = "Public post",
                User = myUser
                
            };
            publicPost.Comment.Add(comment);
            publicPost.Comment.Add(comment);
            publicPost.Comment.Add(comment);
            Post privatePost = new Post()
            {
                Content = "beispieltext",
                CreatedOn = DateTime.Now,
                Description = "Beispielbeschreibung",
                Status = PostStatus.Private,
                Title = "private Post",
                User = myUser
            };
            privatePost.Comment.Add(comment);
            privatePost.Comment.Add(comment);
            privatePost.Comment.Add(comment);

            database.Users.Add(myUser);
            database.Posts.Add(privatePost);
            database.Posts.Add(publicPost);
            database.Users.Add(user);
            database.Comments.Add(comment);
            database.SaveChanges();
            return new HttpStatusCodeResult(200);
        }
    }
}