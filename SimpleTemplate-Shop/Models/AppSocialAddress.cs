using System.ComponentModel.DataAnnotations;

namespace SimpleTemplate_Shop.Models
{
    public class AppSocialAddress
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter URL address")]
        public string UrlAddress { get; set; }
        public string AppSocialImg { get; set; }
    }
}
