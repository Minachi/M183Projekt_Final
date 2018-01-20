using M183_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M183_Blog.ViewModels
{
    public class HomeViewModel
    {
        IEnumerable<Post> posts;
        public HomeViewModel(IEnumerable<Post> posts)
        {
            this.posts = posts;
        }

        public IEnumerable<Post> Posts
        {
            get
            {
                return this.posts;
            }
        }
    }
}