using Microsoft.AspNetCore.Mvc;

namespace SimpleTemplate_Shop.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Error()
        {
            return View();
        }
    }
}
