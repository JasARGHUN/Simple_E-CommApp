using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SimpleTemplate_Shop.Models.ViewModels
{
    public class LoginModel : IdentityUser
    {
        [Required]
        [EmailAddress]
        public string Name { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
