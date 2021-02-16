using Microsoft.AspNetCore.Mvc;
using SimpleTemplate_Shop.Repository.IRepository;

namespace SimpleTemplate_Shop.Components
{
    public class SocialAddressViewComponent : ViewComponent
    {
        private IAppDataRepository _context;

        public SocialAddressViewComponent(IAppDataRepository context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(_context.GetAllAppSocialAddress());
        }
    }
}
