using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleTemplate_Shop.Models;
using SimpleTemplate_Shop.Models.Repository;
using SimpleTemplate_Shop.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTemplate_Shop.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        public int pageSize = 9; // How many objects are on the List page.
        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> List(int? product, string name, string category, int page = 1,
            SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Product> items = _repository.Products.Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID);

            if (!String.IsNullOrEmpty(name))
            {
                items = items.Where(p => p.Name.Contains(name));
            }

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    items = items.OrderByDescending(s => s.Name); break;
                case SortState.PriceAsc:
                    items = items.OrderBy(s => s.ProductPrice); break;
                case SortState.PriceDesc:
                    items = items.OrderByDescending(s => s.ProductPrice); break;
                case SortState.CategoryAsc:
                    items = items.OrderBy(s => s.Category); break;
                case SortState.CategoryDesc:
                    items = items.OrderByDescending(s => s.Category); break;
                default:
                    items = items.OrderBy(s => s.Name); break;
            }

            var count = await items.CountAsync();
            var item = await items.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PagingInfo pagingInfo = new PagingInfo(count, page, pageSize);

            ProductsListViewModel productsListView = new ProductsListViewModel
            {
                PagingInfo = pagingInfo,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(_repository.Products.ToList(), product, name),
                Products = item
            };

            return View(productsListView);
        }
        

        public async Task<ViewResult> Details(int? id)
        {
            Product product = await _repository.GetProduct(id.Value);

            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }

            return View(product);
        }
    }
}
