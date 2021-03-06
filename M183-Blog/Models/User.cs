﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace M183_Blog.Models
{
    public class User
    {
       
        public User()
        {
            this.Comments = new HashSet<Comment>();
            this.Posts = new HashSet<Post>();
            this.Tokens = new HashSet<Token>();
            this.Userlogins = new HashSet<UserLogin>();
            this.UserLogs = new HashSet<UserLog>();
        }
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Familyname { get; set; }
        public string Mobilephonenumber { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
        public virtual ICollection<UserLogin> Userlogins { get; set; }
        public virtual ICollection<UserLog> UserLogs { get; set; }
    }
}