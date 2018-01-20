using M183_Blog.Models;
using M183_Blog.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M183_Blog
{
    public class PostRepo : Repository
    {
        public PostRepo(DataContext db)
            : base(db)
        {
        }

        public IEnumerable<Post> GetPosts()
        {
            return this.db.Posts.ToList();
        }

        public IEnumerable<Post> GetPublicPosts()
        {
            return this.db.Posts.Where(p => p.Status == PostStatus.Public).ToList();
        }

        public IEnumerable<Post> GetPrivatePosts()
        {
            return this.db.Posts.Where(p => p.Status == PostStatus.Private).ToList();
        }

    }
}