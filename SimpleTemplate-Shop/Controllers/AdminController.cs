using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SimpleTemplate_Shop.Models;
using SimpleTemplate_Shop.Models.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using ReflectionIT.Mvc.Paging;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using SimpleTemplate_Shop.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleTemplate_Shop.Repository.IRepository;

namespace SimpleTemplate_Shop.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IInfoRepository _infoRepository;
        private readonly IAppAddressRepository _addressRepository;
        private readonly IAppDataRepository _appRepository;

        public int productInPage = 12; // Products/Objects count in AllProducts page
        public int viewCallBackItemsInPages = 20; // Callback items count in CallBackList page

        public AdminController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment, IInfoRepository infoRepository,
            IAppAddressRepository addressRepository, IAppDataRepository appRepository)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _infoRepository = infoRepository;
            _addressRepository = addressRepository;
            _appRepository = appRepository;
        }

        public ViewResult Index()
        {
            var model = _unitOfWork.Product.GetAll().ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult SeedDatabase()
        {
            SeedData.EnsurePopulated(HttpContext.RequestServices);
            return RedirectToAction(nameof(Index));
        }

        public ViewResult AllProducts(string filter, int page = 1, string sortExpression = "Name")
        {
            var qry = _unitOfWork.Product.GetAll(includeProperties: "Category");

            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.Name.Contains(filter));
            }

            var model = PagingList.Create(qry, productInPage, page, sortExpression, "Name");

            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            model.Action = "AllProducts";

            return View(model);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ProductViewModel item = new ProductViewModel()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null)
            {
                // This code for create
                return View(item);
            }

            // This code for edit
            item.Product = await _unitOfWork.Product.Get(id.GetValueOrDefault());

            if (item.Product == null)
            {
                return NotFound();
            }
            return View(item);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductViewModel item)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\products");
                    var extenstion = Path.GetExtension(files[0].FileName);

                    if (item.Product.Image != null)
                    {
                        // Update data with image
                        var imagePath = Path.Combine(webRootPath, item.Product.Image.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        await files[0].CopyToAsync(fileStreams);
                    }

                    item.Product.Image = @"\images\products\" + fileName + extenstion;
                }
                else
                {
                    // Update data without update image
                    if (item.Product.Id != 0)
                    {
                        Product model = await _unitOfWork.Product.Get(item.Product.Id);
                        item.Product.Image = model.Image;
                    }
                }

                if (item.Product.Id == 0)
                {
                    await _unitOfWork.Product.Add(item.Product);
                }
                else
                {
                    await _unitOfWork.Product.UpdateAsync(item.Product);
                }

                _unitOfWork.Save();

                //TempData[SD.Success] = $"Completed!";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                item.CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

                if (item.Product.Id != 0)
                {
                    item.Product = await _unitOfWork.Product.Get(item.Product.Id);
                }
            }

            return View(item);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductViewModel item = new ProductViewModel()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel item)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\products");
                    var extenstion = Path.GetExtension(files[0].FileName);

                    //if (item.Product.Image != null)
                    //{
                    //    // Update data with image
                    //    var imagePath = Path.Combine(webRootPath, item.Product.Image.TrimStart('\\'));

                    //    if (System.IO.File.Exists(imagePath))
                    //    {
                    //        System.IO.File.Delete(imagePath);
                    //    }
                    //}

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        await files[0].CopyToAsync(fileStreams);
                    }

                    item.Product.Image = @"\images\products\" + fileName + extenstion;
                }
                //else
                //{
                //    // Update data without update image
                //    if (item.Product.Id != 0)
                //    {
                //        Product model = await _unitOfWork.Product.Get(item.Product.Id);
                //        item.Product.Image = model.Image;
                //    }
                //}

                if (item.Product.Id == 0)
                {
                    await _unitOfWork.Product.Add(item.Product);
                }

                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                item.CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

                if (item.Product.Id != 0)
                {
                    item.Product = await _unitOfWork.Product.Get(item.Product.Id);
                }
            }

            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ProductViewModel item = new ProductViewModel()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            item.Product = await _unitOfWork.Product.Get(id);

            if (item.Product == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel item)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\products");
                    var extenstion = Path.GetExtension(files[0].FileName);

                    if (item.Product.Image != null)
                    {
                        // Update data with image
                        var imagePath = Path.Combine(webRootPath, item.Product.Image.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        await files[0].CopyToAsync(fileStreams);
                    }

                    item.Product.Image = @"\images\products\" + fileName + extenstion;
                }
                else
                {
                    // Update data without update image
                    if (item.Product.Id != 0)
                    {
                        Product model = await _unitOfWork.Product.Get(item.Product.Id);
                        item.Product.Image = model.Image;
                    }
                }

                await _unitOfWork.Product.UpdateAsync(item.Product);

                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                item.CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

                if (item.Product.Id != 0)
                {
                    item.Product = await _unitOfWork.Product.Get(item.Product.Id);
                }
            }

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int productId)
        {
            var model = await _unitOfWork.Product.Get(productId);
            if (model != null)
            {
                TempData["message"] = $"{model.Name} was deleted";
            }

            string webRootPath = _hostingEnvironment.WebRootPath;

            if (model.Image != null)
            {
                var imagePath = Path.Combine(webRootPath, model.Image.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            await _unitOfWork.Product.Remove(model);
            _unitOfWork.Save();

            return RedirectToAction("AllProducts");
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Product.GetAll();
            return Json(new { data = allObj });
        }

        /*
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _unitOfWork.Product.Get(id);

            if (model == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            string webRootPath = _hostingEnvironment.WebRootPath;

            var imagePath = Path.Combine(webRootPath, model.Image.TrimStart('\\'));

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            await _unitOfWork.Product.Remove(model);
            _unitOfWork.Save();

            return Json(new { success = true, message = $"The object {model.Name} has been deleted" });
        }
        */
        #endregion

        #region App Info

        //Application information
        [Authorize(Roles = SD.Role_Admin)]
        [HttpGet]
        public ViewResult CreateInfo()
        {
            return View();
        }

        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        public IActionResult CreateInfo(InfoCreateViewModel model) //НЕИСПОЛЬЗУЕТСЯ!
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadAppImage(model);
                string uniqueHomeFileName = ProcessUploadAppHomeImage(model);
                string uniqueHomeFileNameFirst = ProcessUploadAppHomeImageFirst(model);
                string uniqueHomeFileNameSecond = ProcessUploadAppHomeImageSecond(model);
                var newInfo = new Info
                {
                    AppName = model.AppName,
                    AppImage = uniqueFileName,
                    AppHomeImage = uniqueHomeFileName,
                    AppHomeImageFirst = uniqueHomeFileNameFirst,
                    AppHomeImageSecond = uniqueHomeFileNameSecond,
                    AppHomeImageText = model.AppHomeImageText,
                    AppHomeImageTextFirst = model.AppHomeImageTextFirst,
                    AppHomeImageTextSecond = model.AppHomeImageTextSecond
                };
                _infoRepository.Add(newInfo);
                TempData["message"] = $"{model.AppName} был создан.";
                return RedirectToAction("Index");
            }

            return View();
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [HttpGet]
        public ViewResult EditInfo() //получаем текущие данные о приложении(Название, логотип, контакты и тд. приложения).
        {
            Info info = _infoRepository.GetInfo(1);
            InfoEditViewModel infoEditViewModel = new InfoEditViewModel
            {
                Id = info.InfoID,
                AppName = info.AppName, //имя приложения
                ExistingImagePath = info.AppImage, //логотип
                ExistingAppHomeImage = info.AppHomeImage, //изображение домашней страницы
                ExistingAppHomeImageFirst = info.AppHomeImageFirst, //изображение домашней страницы(1-блок)
                ExistingAppHomeImageSecond = info.AppHomeImageSecond, //изображение домашней страницы(2-блок)
                AppHomeImageText = info.AppHomeImageText,
                AppHomeImageTextFirst = info.AppHomeImageTextFirst,
                AppHomeImageTextSecond = info.AppHomeImageTextSecond,
            };
            return View(infoEditViewModel);
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [HttpPost]
        public IActionResult EditInfo(InfoEditViewModel model) //отправляем обновленные данные о приложении в Базу Данных(Название, логотип, контакты и тд. приложения).
        {
            if (ModelState.IsValid)
            {
                Info info = _infoRepository.GetInfo(model.Id = 1);
                info.AppName = model.AppName;
                info.AppHomeImageText = model.AppHomeImageText;
                info.AppHomeImageTextFirst = model.AppHomeImageTextFirst;
                info.AppHomeImageTextSecond = model.AppHomeImageTextSecond;

                if (model.AppImages != null) //обновление изображения логотипа сайта.
                {
                    if (model.ExistingImagePath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingImagePath);
                        System.IO.File.Delete(filePath);
                    }
                    info.AppImage = ProcessUploadAppImage(model);
                }
                
                if(model.AppHomeImage != null) //обновление изображение домашней страницы.
                {
                    if(model.ExistingAppHomeImage != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingAppHomeImage);
                        System.IO.File.Delete(filePath);
                    }
                    info.AppHomeImage = ProcessUploadAppHomeImage(model);
                }

                if (model.AppHomeImageFirst != null) //обновление изображение домашней страницы.
                {
                    if (model.ExistingAppHomeImageFirst != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingAppHomeImageFirst);
                        System.IO.File.Delete(filePath);
                    }
                    info.AppHomeImageFirst = ProcessUploadAppHomeImageFirst(model);
                }

                if (model.AppHomeImageSecond != null) //обновление изображение домашней страницы.
                {
                    if (model.ExistingAppHomeImageSecond != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingAppHomeImageSecond);
                        System.IO.File.Delete(filePath);
                    }
                    info.AppHomeImageSecond = ProcessUploadAppHomeImageSecond(model);
                }

                _infoRepository.Update(info);
                TempData["message"] = $"Информация о приложения {model.AppName} была отредактирована.";
                return RedirectToAction("Index");
            }
            return View();
        }
        private string ProcessUploadAppImage(InfoCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.AppImages != null && model.AppImages.Count > 0)
            {
                foreach (IFormFile photo in model.AppImages)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }

            return uniqueFileName;
        }
        private string ProcessUploadAppHomeImage(InfoCreateViewModel model)
        {
            string uniqueFileName = null;
            if(model.AppHomeImage != null && model.AppHomeImage.Count > 0)
            {
                foreach(IFormFile img in model.AppHomeImage)
                {
                    var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + img.FileName;
                    var filePath = Path.Combine(uploadFolder, uniqueFileName);
                    using(var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        img.CopyTo(fileStream);
                    }
                }
            }
            return uniqueFileName;
        }
        private string ProcessUploadAppHomeImageFirst(InfoCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.AppHomeImageFirst != null && model.AppHomeImageFirst.Count > 0)
            {
                foreach (IFormFile img in model.AppHomeImageFirst)
                {
                    var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + img.FileName;
                    var filePath = Path.Combine(uploadFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        img.CopyTo(fileStream);
                    }
                }
            }
            return uniqueFileName;
        }
        private string ProcessUploadAppHomeImageSecond(InfoCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.AppHomeImageSecond != null && model.AppHomeImageSecond.Count > 0)
            {
                foreach (IFormFile img in model.AppHomeImageSecond)
                {
                    var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + img.FileName;
                    var filePath = Path.Combine(uploadFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        img.CopyTo(fileStream);
                    }
                }
            }
            return uniqueFileName;
        }
        #endregion

        #region Social Link
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [HttpGet]
        public IActionResult CreateSocialData()
        {
            return View();
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [HttpPost]
        public IActionResult CreateSocialData(AppSocialLinkCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueSocialFileName = ProcessUploadAppSocialImage(model);

                var newAppSocialAddress = new AppSocialAddress
                {
                    UrlAddress = model.UrlAddress,
                    AppSocialImg = uniqueSocialFileName
                };

                _appRepository.Add(newAppSocialAddress);

                TempData["message"] = $"Link {model.UrlAddress} was created.";
                return RedirectToAction("SocialList");
            }
            return View();
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [HttpGet]
        public IActionResult EditSocialData(int id)
        {
            var data = _appRepository.GetInfo(id);

            var item = new AppSocialLinkUpdateViewModel
            {
                Id = data.Id,
                UrlAddress = data.UrlAddress,
                ExistingSocialImagePath = data.AppSocialImg
            };

            TempData["message"] = $"Object {item.UrlAddress} was selected.";
            return View(item);
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [HttpPost]
        public IActionResult EditSocialData(AppSocialLinkUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = _appRepository.GetInfo(model.Id);
                item.UrlAddress = model.UrlAddress;

                if (model.AppSocialImgs != null)
                {
                    if (model.ExistingSocialImagePath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingSocialImagePath);
                        System.IO.File.Delete(filePath);
                    }
                    item.AppSocialImg = ProcessUploadAppSocialImage(model);
                }

                _appRepository.Update(item);

                TempData["message"] = $"Object {item.UrlAddress} was edited.";

                return RedirectToAction("SocialList");
            }
            return View();
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [HttpPost]
        public async Task<IActionResult> DeleteSocialAddress(int id)
        {
            var item = await _appRepository.Delete(id);
            if (item != null)
            {
                TempData["message"] = $"Url address: {item.UrlAddress} was deleted";
            }
            return RedirectToAction("SocialList");
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult SocialList()
        {
            var obj = _appRepository.AppSocialAddress;
            return View(obj);
        }

        private string ProcessUploadAppSocialImage(AppSocialLinkCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.AppSocialImgs != null && model.AppSocialImgs.Count > 0)
            {
                foreach (IFormFile photo in model.AppSocialImgs)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }

            return uniqueFileName;
        }
        #endregion

        #region Address

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [HttpGet]
        public IActionResult CreateAppAddressData()
        {
            return View();
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [HttpPost]
        public async Task<IActionResult> CreateAppAddressData(AppAddressCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniquePictureFileName = ProcessUploadAppAddressImage(model);

                var newAppAddress = new AppAddress
                {
                    Address = model.Address,
                    Picture = uniquePictureFileName,
                    Phone = model.Phone,
                    City = model.City,
                    Description = model.Description,
                    Email = model.Email
                };

                await _addressRepository.AddAsync(newAppAddress);

                TempData["message"] = $"Address {model.Address} was created.";
                return RedirectToAction("AddressList");
            }
            return View();
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [HttpGet]
        public async Task<IActionResult> EditAppAddressData(int id)
        {
            var model = await _addressRepository.GetInfoAsync(id);

            var item = new AppAddressUpdateViewModel
            {
                Id = model.Id,
                Address = model.Address,
                Phone = model.Phone,
                City = model.City,
                Email = model.Email,
                Description = model.Description,
                AppPicturePath = model.Picture
            };

            TempData["message"] = $"Object {item.Address} was selected.";
            return View(item);
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [HttpPost]
        public async Task<IActionResult> EditAppAddressData(AppAddressUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = await _addressRepository.GetInfoAsync(model.Id);

                item.Address = model.Address;
                item.City = model.City;
                item.Phone = model.Phone;
                item.Email = model.Email;
                item.Description = model.Description;

                if (model.AppPicture != null)
                {
                    if (model.AppPicturePath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.AppPicturePath);
                        System.IO.File.Delete(filePath);
                    }
                    item.Picture = ProcessUploadAppAddressImage(model);
                }

                await _addressRepository.UpdateAsync(item);

                TempData["message"] = $"Object {item.Address} was edited.";

                return RedirectToAction("AddressList");
            }
            return View();
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [HttpPost]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var item = await _addressRepository.DeleteAsync(id);

            if (item != null)
            {
                TempData["message"] = $"Url address: {item.Address} was deleted";
            }

            return RedirectToAction("AddressList");
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult AddressList()
        {
            var obj = _addressRepository.AppAddress;
            return View(obj);
        }

        private string ProcessUploadAppAddressImage(AppAddressCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.AppPicture != null && model.AppPicture.Count > 0)
            {
                foreach (IFormFile photo in model.AppPicture)
                {
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }

            return uniqueFileName;
        }
        #endregion

        #region CallBack

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult CallBackList(string filter, int page = 1, string sortExpression = "CallTime")
        {
            var qry = _unitOfWork.CallBack.GetAll().AsQueryable(); //_repository.Orders.AsNoTracking().Where(o => !o.Shipped);
            var model = PagingList.Create(qry, viewCallBackItemsInPages, page, sortExpression, "CallTime");

            return View(model);
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public async Task<ViewResult> CallBackDetails(int? id)
        {
            var product = await _unitOfWork.CallBack.Get(id.Value);

            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCallBack(int id)
        {
            var model = await _unitOfWork.CallBack.Get(id);

            if (model != null)
            {
                TempData["message"] = $"{model.Id} was deleted";
            }

            await _unitOfWork.CallBack.Remove(model);
            _unitOfWork.Save();

            return RedirectToAction(nameof(CallBackList));
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult Marked(int id)
        {
            var model = _unitOfWork.CallBack
                .GetFirstOrDefault(x => x.Id == id);

            if (model != null)
            {
                model.Marked = true;
                _unitOfWork.Save();
            }

            return RedirectToAction(nameof(CallBackList));
        }
        #endregion
    }
}
