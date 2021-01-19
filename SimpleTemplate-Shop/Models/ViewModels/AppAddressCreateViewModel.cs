using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleTemplate_Shop.Models.ViewModels
{
    public class AppAddressCreateViewModel
    {
        public string City { get; set; }
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public List<IFormFile> AppPicture { get; set; }

        [MaxLength(300, ErrorMessage = "Only 300 characters")]
        public string Description { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
