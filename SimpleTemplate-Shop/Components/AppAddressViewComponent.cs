using Microsoft.AspNetCore.Mvc;
using SimpleTemplate_Shop.Models.Repository;


namespace SimpleTemplate_Shop.Components
{
    public class AppAddressViewComponent : ViewComponent
    {
        private readonly IAppAddressRepository _repository;

        public AppAddressViewComponent(IAppAddressRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            return View(_repository.GetAllAppAddress());
        }
    }
}
