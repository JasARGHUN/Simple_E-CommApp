using Microsoft.AspNetCore.Mvc;
using SimpleTemplate_Shop.Models;

namespace SimpleTemplate_Shop.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart cart;

        public CartSummaryViewComponent(Cart cartService)
        {
            cart = cartService;
        }

        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
