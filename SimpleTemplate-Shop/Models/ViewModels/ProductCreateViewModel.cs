using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleTemplate_Shop.Models.ViewModels
{
    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "Object name field can't be empty...")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description field can't be empty...")]
        [MaxLength(800, ErrorMessage = "Only 800 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price field can't be empty...")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Manufacturer field can't be empty...")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "Manufacturing date field can't be empty...")]
        public string DateOfManufacture { get; set; }

        [Required(ErrorMessage = "This field can't be empty...")]
        public int QuantityInStock { get; set; }

        public string Category { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<IFormFile> Images2 { get; set; }
        public List<IFormFile> Images3 { get; set; }
    }
}
