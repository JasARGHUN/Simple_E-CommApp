using Microsoft.AspNetCore.Mvc;
using SimpleTemplate_Shop.Models;
using System.Threading.Tasks;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Authorization;
using SimpleTemplate_Shop.Infrastructure;
using System.Linq;
using SimpleTemplate_Shop.Repository.IRepository;

namespace SimpleTemplate_Shop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public int itemPerPage = 10;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        // GET: Admin/Category
        public IActionResult Index(int page = 1, string sortExpression = "Name")
        {
            var qry = _unitOfWork.Category.GetAll();
            var items = PagingList.Create(qry, itemPerPage, page, sortExpression, "Name");

            return View(items);
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public async Task<IActionResult> Upsert(int? id)
        {
            var category = new Category();
            if (id == null)
            {
                // This code for create
                return View(category);
            }
            // This code for edit
            category = await _unitOfWork.Category.Get(id.GetValueOrDefault());

            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Category item)
        {
            if (ModelState.IsValid)
            {
                if (item.Id == 0)
                {
                    await _unitOfWork.Category.Add(item);
                }
                else
                {
                    await _unitOfWork.Category.UpdateAsync(item);
                }
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        #region API CALLS

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _unitOfWork.Category.Get(id);

            if (model != null)
            {
                TempData["message"] = $"{model.Name} was deleted";
            }

            await _unitOfWork.Category.Remove(model);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Category.GetAll();
            return Json(new { data = allObj });
        }

        #endregion
    }
}
