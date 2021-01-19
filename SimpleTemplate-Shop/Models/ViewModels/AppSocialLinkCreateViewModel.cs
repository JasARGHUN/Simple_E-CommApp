using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleTemplate_Shop.Models.ViewModels
{
    public class AppSocialLinkCreateViewModel
    {
        [Required(ErrorMessage = "Enter URL address")]
        public string UrlAddress { get; set; }
        public List<IFormFile> AppSocialImgs { get; set; }
    }
}
