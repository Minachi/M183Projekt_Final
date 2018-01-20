using M183_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M183_Blog.Repositories
{
    public class TokenRepository
    {
        DataContext db;
        public TokenRepository(DataContext db)
        {
            this.db = db;
        }

        public bool VerifyToken(int token, int userId)
        {
            try
            {
                db.Tokens.First(t => t.TokenNr == token && t.UserId == userId && t.Expiry > DateTime.Now);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}