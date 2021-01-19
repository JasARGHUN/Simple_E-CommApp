using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SimpleTemplate_Shop.Models;
using SimpleTemplate_Shop.Models.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using SimpleTemplate_Shop.Models.Repository;
using ReflectionIT.Mvc.Paging;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using SimpleTemplate_Shop.Infrastructure;

namespace SimpleTemplate_Shop.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IInfoRepository _infoRepository;
        private readonly IAppAddressRepository _addressRepository;
        private readonly IAppDataRepository _appRepository;

        public int productInPage = 12; // Products/Objects count in AllProducts page

        public AdminController(IProductRepository repository, IWebHostEnvironment hostingEnvironment, IInfoRepository infoRepository,
            IAppAddressRepository addressRepository, IAppDataRepository appRepository)
        {
            _repository = repository;
            _hostingEnvironment = hostingEnvironment;
            _infoRepository = infoRepository;
            _addressRepository = addressRepository;
            _appRepository = appRepository;
        }

        public ViewResult Index()
        {
            var model = _repository.Products.ToList();

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
            var qry = _repository.Products.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.Name.Contains(filter));
            }

            var model = PagingList.Create(qry, productInPage, page, sortExpression, "Name");

            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            model.Action = "AllProducts";

            return View(model);
        }


        #region Product
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [HttpGet]
        public async Task<ViewResult> Edit(int ProductID)
        {
            Product product = await _repository.GetProduct(ProductID);
            ProductEditViewModel productEditViewModel = new ProductEditViewModel
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Category = product.Category,
                Description = product.ProductDescription,
                Price = product.ProductPrice,
                Manufacturer = product.Manufacturer,
                DateOfManufacture = product.DateOfManufacture,
                QuantityInStock = product.QuantityInStock,
                ExistingPhotoPath = product.Image,
                ExistingPhotoPath2 = product.Image2,
                ExistingPhotoPath3 = product.Image3
            };
            TempData["message"] = $"Object {product.Name} was selected.";
            return View(productEditViewModel);
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = await _repository.GetProduct(model.ProductID);
                product.Name = model.Name;
                product.ProductPrice = model.Price;
                product.ProductDescription = model.Description;
                product.Category = model.Category;
                product.Manufacturer = model.Manufacturer;
                product.DateOfManufacture = model.DateOfManufacture;
                product.QuantityInStock = model.QuantityInStock;

                if (model.Images != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    product.Image = ProcessUploadFile(model);
                }
                if (model.Images2 != null)
                {
                    if (model.ExistingPhotoPath2 != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath2);
                        System.IO.File.Delete(filePath);
                    }

                    product.Image2 = ProcessUploadFile2(model);
                }
                if (model.Images3 != null)
                {
                    if (model.ExistingPhotoPath3 != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath3);
                        System.IO.File.Delete(filePath);
                    }

                    product.Image3 = ProcessUploadFile3(model);
                }
                _repository.SaveProduct(product);
                TempData["message"] = $"Object {product.Name} was edited.";
                return RedirectToAction("AllProducts");
            }
            return View();
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [HttpPost]
        public IActionResult Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(model);
                string uniqueFileName2 = ProcessUploadFile2(model);
                string uniqueFileName3 = ProcessUploadFile3(model);
                var newProduct = new Product
                {
                    Name = model.Name,
                    ProductDescription = model.Description,
                    ProductPrice = model.Price,
                    Category = model.Category,
                    Manufacturer = model.Manufacturer,
                    DateOfManufacture = model.DateOfManufacture,
                    QuantityInStock = model.QuantityInStock,
                    Image = uniqueFileName,
                    Image2 = uniqueFileName2,
                    Image3 = uniqueFileName3
                };

                _repository.Add(newProduct);
                TempData["message"] = $"{newProduct.Name} has be created";
                return RedirectToAction("AllProducts");
            }

            return View();
        }

        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
        [HttpPost]
        public async Task<IActionResult> Delete(int productId)
        {
            Product deleteProduct = await _repository.DeleteProduct(productId);
            if (deleteProduct != null)
            {
                TempData["message"] = $"{deleteProduct.Name} was deleted";
            }
            return RedirectToAction("AllProducts");
        }

        private string ProcessUploadFile(ProductCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Images != null && model.Images.Count > 0)
            {
                foreach (IFormFile photo in model.Images)
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
        private string ProcessUploadFile2(ProductCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Images2 != null && model.Images2.Count > 0)
            {
                foreach (IFormFile photo in model.Images2)
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
        private string ProcessUploadFile3(ProductCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Images3 != null && model.Images3.Count > 0)
            {
                foreach (IFormFile photo in model.Images3)
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
    }
}
