using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleTemplate_Shop.Models;
using SimpleTemplate_Shop.Models.Repository;
using SimpleTemplate_Shop.Models.ViewModels;
using System.Threading.Tasks;

namespace SimpleTemplate_Shop.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _repository;
        private Cart cart;

        public CartController(IProductRepository repository, Cart cartService)
        {
            _repository = repository;
            cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public async Task<RedirectToActionResult> AddToCart(int productId, string returnUrl, decimal sum)
        {
            Product product = await _repository.Products
                .FirstOrDefaultAsync(p => p.ProductID == productId);
            sum = product.ProductPrice;

            if (product != null)
            {
                cart.AddItem(product, 1, sum);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public async Task<RedirectToActionResult> RemoveFromCart(int productId, string returnUrl)
        {
            Product product = await _repository.Products
                .FirstOrDefaultAsync(p => p.ProductID == productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }

        public async Task<IActionResult> Plus(int productId)
        {
            var product = await _repository.Products
                .FirstOrDefaultAsync(p => p.ProductID == productId);

            cart.AddItem(product, 1, product.ProductPrice);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Minus(int productId)
        {
            var product = await _repository.Products
                .FirstOrDefaultAsync(p => p.ProductID == productId);

            cart.RemoveItem(product, 1, product.ProductPrice);

            return RedirectToAction(nameof(Index));
        }
    }
}
