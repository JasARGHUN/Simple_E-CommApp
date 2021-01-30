using System.Collections.Generic;
using System.Linq;

namespace SimpleTemplate_Shop.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity, decimal sum)
        {
            CartLine line = lineCollection
                .Where(p => p.Product.Id == product.Id)
                .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity,
                    TotalSum = sum
                });
            }
            else
            {
                line.Quantity += quantity;
                line.TotalSum += sum;
            }
        }

        public virtual void RemoveItem(Product product, int quantity, decimal sum)
        {
            CartLine line = lineCollection
                .Where(p => p.Product.Id == product.Id)
                .FirstOrDefault();

            line.Quantity -= quantity;
            line.TotalSum -= sum;

            if (line.Quantity == 0)
            {
                RemoveLine(line.Product);
            }               
        }

        public virtual void RemoveLine(Product product) =>
            lineCollection.RemoveAll(l => l.Product.Id == product.Id);

        public virtual decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.ProductPrice * e.Quantity);
        }

        public virtual void Clear() =>
            lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines =>
            lineCollection;
    }
}
