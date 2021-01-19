using System.ComponentModel.DataAnnotations;

namespace SimpleTemplate_Shop.Models
{
    public class Info
    {
        public int InfoID { get; set; }

        [Required()]
        public string AppName { get; set; }

        public string AppImage { get; set; }

        public string AppHomeImage { get; set; }

        [Required()]
        [MaxLength(1200, ErrorMessage = "Only 1200 characters")]
        public string AppHomeImageText { get; set; }
        public string AppHomeImageFirst { get; set; }

        [Required()]
        [MaxLength(800, ErrorMessage = "Only 800 characters")]
        public string AppHomeImageTextFirst { get; set; }
        public string AppHomeImageSecond { get; set; }

        [Required()]
        [MaxLength(800, ErrorMessage = "Only 800 characters")]
        public string AppHomeImageTextSecond { get; set; }
    }
}
