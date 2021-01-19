using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SimpleTemplate_Shop.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [BindNever]
        public bool Shipped { get; set; }

        [Required()]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Address field is required.")]
        public string Line1 { get; set; }

        [Required]
        [EmailAddress()]
        public string Email { get; set; }

        [Required()]
        public string City { get; set; }

        [Required()]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required()]
        public string Zip { get; set; }

        public DateTime orderTime = DateTime.Now;

        public DateTime OrderTime
        {
            get { return orderTime; }
            set { orderTime = value; }

        }

        public decimal TotalAmount { get; set; }
    }
}
