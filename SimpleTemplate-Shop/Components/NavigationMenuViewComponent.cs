using Microsoft.AspNetCore.Mvc;
using SimpleTemplate_Shop.Models.Repository;
using System.Linq;

namespace SimpleTemplate_Shop.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository _repository;

        public NavigationMenuViewComponent(IProductRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
