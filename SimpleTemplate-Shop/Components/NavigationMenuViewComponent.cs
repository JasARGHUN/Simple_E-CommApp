using Microsoft.AspNetCore.Mvc;
using SimpleTemplate_Shop.Repository.IRepository;
using System.Linq;

namespace SimpleTemplate_Shop.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IUnitOfWork _repository;

        public NavigationMenuViewComponent(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            var item = _repository.Product.GetAll()
                .Select(x => x.Category.Name)
                .Distinct()
                .OrderBy(x => x);

            return View(item);
        }
    }
}
