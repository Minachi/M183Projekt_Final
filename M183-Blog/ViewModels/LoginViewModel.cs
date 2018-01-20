using System.ComponentModel.DataAnnotations;

namespace M183_Blog.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Nutzername")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Passwort")]
        public string Password { get; set; }

    }
}