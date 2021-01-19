using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleTemplate_Shop.Models.ViewModels
{
    public class InfoCreateViewModel
    {
        [Required()]
        public string AppName { get; set; }
        public List<IFormFile> AppImages { get; set; }
        public List<IFormFile> AppHomeImage { get; set; }
        public List<IFormFile> AppHomeImageFirst { get; set; }
        public List<IFormFile> AppHomeImageSecond { get; set; }

        [Required()]
        [MaxLength(1200, ErrorMessage = "Only 1200 characters")]
        public string AppHomeImageText { get; set; }

        [Required()]
        [MaxLength(800, ErrorMessage = "Only 800 characters")]
        public string AppHomeImageTextFirst { get; set; }

        [Required()]
        [MaxLength(800, ErrorMessage = "Only 800 characters")]
        public string AppHomeImageTextSecond { get; set; }
    }
}
