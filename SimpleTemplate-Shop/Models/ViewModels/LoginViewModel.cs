using System.ComponentModel.DataAnnotations;

namespace SimpleTemplate_Shop.Models.ViewModels
{
    public class LoginViewModel : LoginModel
    {

        [Display(Name = "Remeber?")]
        public bool RememberMe { get; set; }
    }
}
