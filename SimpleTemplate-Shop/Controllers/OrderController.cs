using SimpleTemplate_Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System.Linq;
using System.Threading.Tasks;
using SimpleTemplate_Shop.Infrastructure;
using SimpleTemplate_Shop.Repository.IRepository;

namespace SimpleTemplate_Shop.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _repository;
        private Cart _cart;
        public int orderPages = 10; // How many objects are on the Orders page.
        public int viewOrderDbPages = 10; // How many objects are on the ViewDataBase page.

        public OrderController(IOrderRepository repositoryService, Cart cartService)
        {
            _repository = repositoryService;
            _cart = cartService;
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public ViewResult Index(int page = 1, string sortExpression = "OrderTime")
        {
            var qry = _repository.Orders.AsNoTracking().Where(o => !o.Shipped);
            var model = PagingList.Create(qry, orderPages, page, sortExpression, "OrderTime");

            return View(model);
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee + "," + SD.Role_Preview)]
        [HttpGet]
        public ViewResult ViewDataBase(string filter, int page = 1, string sortExpression = "OrderTime")
        {
            var data = _repository.Orders.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                data = data.Where(d => d.Name.Contains(filter));
            }

            var model = PagingList.Create(data, viewOrderDbPages, page, sortExpression, "OrderTime");

            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            model.Action = "ViewDataBase";

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public async Task<IActionResult> MarkShipped(int orderID)
        {
            Order order = await _repository.Orders
                .FirstOrDefaultAsync(o => o.OrderID == orderID);
            if (order != null)
            {
                order.Shipped = true;
                await _repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(Index));
        }

        public ViewResult Checkout() =>
            View(new Models.Order());

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (_cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Your basket is empty");
            }
            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
                order.TotalAmount = _cart.ComputeTotalValue();
                await _repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }

        public ViewResult Completed()
        {
            _cart.Clear();
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var element = await _repository.Orders.FirstOrDefaultAsync(x => x.OrderID == id);
            await  _repository.Delete(element);

            return RedirectToAction("Index");
        }
    }
}
