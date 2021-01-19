using Microsoft.AspNetCore.Mvc;
using SimpleTemplate_Shop.Models.Repository;

namespace SimpleTemplate_Shop.Components
{
    public class AppHomeImageTextViewComponent : ViewComponent
    {
        private readonly IInfoRepository _infoRepository;
        public AppHomeImageTextViewComponent(IInfoRepository infoRepository)
        {
            _infoRepository = infoRepository;
        }
        public IViewComponentResult Invoke()
        {
            var info = _infoRepository.GetInfo(1);
            return View(info);
        }
    }
}
