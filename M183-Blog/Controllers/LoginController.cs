using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using M183_Blog.Helpers;
using M183_Blog.Models;
using M183_Blog.ViewModels;
using M183_Blog.Services;
using System.Threading.Tasks;
using M183_Blog.Repositories;

namespace M183_Blog.Controllers
{
    public class LoginController : Controller
    {
        public DataContext db = new DataContext();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(x => x.Username == model.Username))
                {
                    User user = db.Users.First(x => x.Username == model.Username);
                    if (SecurePasswordHasher.Verify(model.Password, user.Password))
                    {
                        Token token = new Token()
                        {
                            Expiry = DateTime.Now.AddMinutes(5),
                            TokenNr = new Random().Next(1, 999999),
                            UserId = user.Id,
                            User = user
                        };
                        db.Tokens.Add(token);
                        db.SaveChanges();
                        new NexmoService().SendSMS(token.TokenNr, user.Mobilephonenumber);
                        ViewBag.Status = "sms_sent";
                        return
                            View("TokenLogin",
                                new TokenViewModel()
                                {
                                    UserId = user.Id
                                });
                    }
                    
                    LogUserAction("Wrong password", user.Id);
                    ModelState.AddModelError("Password", "wrong Passwort");
                    return View("Index", model);
                }
                else
                {
                    LogUserAction("Login failed", null);
                    ModelState.AddModelError("Username", "no such User found.");
                    return View("Index", model);
                }
            }
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult TokenLogin(TokenViewModel model)
        {
            TokenRepository tokenRepository = new TokenRepository(db);
            if (tokenRepository.VerifyToken(model.Token, model.UserId))
            {
                LogUserAction("Login successful", model.UserId);
                return View("Index"); // Todo: Goto User Page
            }
            LogUserAction("Invalid token", model.UserId);
            ViewBag.Status = "invalid_token";
            ModelState.AddModelError("Token", "Token is not valid");
            return View("TokenLogin", model);
        }

        private void LogUserAction(string action, int? userId)
        {
            db.UserLogs.Add(new UserLog(action, db.Users.FirstOrDefault(x => x.Id.Equals(userId.Value))));                
            db.SaveChanges();
        }
    }
}