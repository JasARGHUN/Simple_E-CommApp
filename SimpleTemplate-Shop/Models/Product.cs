using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleTemplate_Shop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Object name field can't be empty...")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description field can't be empty...")]
        [MaxLength(800, ErrorMessage = "Only 800 characters")]
        public string ProductDescription { get; set; }

        public string Category { get; set; }

        [Required(ErrorMessage = "Price field can't be empty...")]
        public decimal ProductPrice { get; set; }

        [Required(ErrorMessage = "Manufacturer field can't be empty...")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "Manufacturing date field can't be empty...")]
        public string DateOfManufacture { get; set; }

        [Required(ErrorMessage = "This field can't be empty...")]
        public int QuantityInStock { get; set; }

        public string Image { get; set; }

        // Product Description
        [Required]
        public string Type { get; set; }
        [Required]
        public string Processor { get; set; }
        [Required]
        public string RAM { get; set; }
        [Required]
        public string PowerSupply { get; set; }
        [Required]
        public string StorageDevice { get; set; }
        [Required]
        public string VideoCard { get; set; }
        [Required]
        public string OperatingSystem { get; set; }
    }
}
