using System;

namespace SimpleTemplate_Shop.Models
{
    public class CartLine
    {
        public int CartLineID { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalSum { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.Now;
    }
}
