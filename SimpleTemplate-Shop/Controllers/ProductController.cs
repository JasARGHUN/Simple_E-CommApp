using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SimpleTemplate_Shop.Models;
using SimpleTemplate_Shop.Models.ViewModels;
using SimpleTemplate_Shop.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTemplate_Shop.Controllers
{
    public class ProductController : Controller
    {
        private IUnitOfWork _repository;
        public int pageSize = 9; // How many objects are on the List page.
        public ProductController(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> List(int? product, string name, string category, int page = 1,
            SortState sortOrder = SortState.NameAsc)
        {
            IEnumerable<Product> items = _repository.Product.GetAll(includeProperties: "Category").Where(p => category == null || p.Category.Name == category)
                .OrderBy(p => p.Id);

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
                    items = items.OrderBy(s => s.Category.Name); break;
                case SortState.CategoryDesc:
                    items = items.OrderByDescending(s => s.Category.Name); break;
                default:
                    items = items.OrderBy(s => s.Name); break;
            }

            var count = items.Count();
            var item = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PagingInfo pagingInfo = new PagingInfo(count, page, pageSize);

            ProductViewModel productsListView = new ProductViewModel
            {
                PagingInfo = pagingInfo,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(_repository.Product.GetAll().ToList(), product, name),
                Products = item
            };

            return View(productsListView);
        }


        public async Task<ViewResult> Details(int? id)
        {
            var product = _repository.Product.GetFirstOrDefault(x => x.Id == id.Value, includeProperties: "Category");

            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }

            return View(product);
        }
    }
}
