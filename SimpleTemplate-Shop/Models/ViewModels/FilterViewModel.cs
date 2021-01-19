using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SimpleTemplate_Shop.Models.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Product> products, int? product, string name)
        {
            products.Insert(0, new Product { Name = "All", ProductID = 0 });
            Products = new SelectList(products, "ProductID", "Name", product);
            SelectedProduct = product;
            SelectedName = name;
        }
        public SelectList Products { get; private set; }
        public int? SelectedProduct { get; private set; }
        public string SelectedName { get; private set; } 
        public string SelectedCategory { get; private set; }
        public string SelectedManufacturer { get; private set; }
    }
}
