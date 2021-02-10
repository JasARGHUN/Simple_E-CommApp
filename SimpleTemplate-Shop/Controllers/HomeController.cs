using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleTemplate_Shop.Models;
using SimpleTemplate_Shop.Models.Repository.IRepository;
using SimpleTemplate_Shop.Models.ViewModels;

namespace SimpleTemplate_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region CallBack
        [HttpPost]
        public async Task<IActionResult> Contacts(CallBackViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.CallBack.Add(model.CallBack);
                _unitOfWork.Save();
            }
            return RedirectToAction("CallBackCompleted");
        }

        public IActionResult CallBackCompleted()
        {
            return View();
        }
        #endregion

    }
}
