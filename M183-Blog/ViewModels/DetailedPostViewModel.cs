using M183_Blog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M183_Blog.ViewModels
{
    public class DetailedPostViewModel
    {
        Post post;
        IEnumerable<Comment> comments;

        public DetailedPostViewModel(Post post)
        {
            this.post = post;
            this.comments = post.Comment;
        }

        public DetailedPostViewModel()
        {

        }

        public IEnumerable<Comment> Comments
        {
            get
            {
                return this.comments;
            }
        }
        public Post Post
        {
            get
            {
                return this.post;
            }
            set
            {
                this.post = value;
            }
        }

        
    }
}