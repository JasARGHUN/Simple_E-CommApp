using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SimpleTemplate_Shop.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public string Name { get; set; }

        public PagingInfo PagingInfo { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
