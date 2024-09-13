using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using ClickMart.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Logging.Abstractions;
using System;


namespace ClickMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<User> _userManager;
        public ProductController(IUnitOfWork unitOfWork
            ,IWebHostEnvironment webHostEnvironment,
            UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = _unitOfWork.Product.GetAll(IncludeProperties:"Category").ToList();
            ViewData["User"] = await _userManager.GetUserAsync(User);
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(string? Id)
        {
            IEnumerable<SelectListItem> Categories = _unitOfWork.Category.GetAll().Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id,
                });
            ViewBag.Categories = Categories;
            ViewData["User"] = await _userManager.GetUserAsync(User);
            if (Id == null)
            {
                return View();
            }
            else
            {
                Product product = _unitOfWork.Product.GetOrDefalut(x => x.Id == Id
                , IncludeProperties: "Galleries");
                return View(product);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(string? Id,Product product,string? imageUrls,IFormFile? thumbnail)
        {
            if (ModelState.IsValid)
            {
                if (thumbnail != null)
                {
                    string thumbnailName = Guid.NewGuid().ToString() + "_" + thumbnail.FileName;
                    var FilePath = Path.Combine(_webHostEnvironment.WebRootPath, @"Images\Products");
                    if (!Directory.Exists(FilePath))
                    {
                        Directory.CreateDirectory(FilePath);
                    }

                    var filePath = Path.Combine(FilePath, thumbnailName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        thumbnail.CopyTo(fileStream);
                    }
                    Galleries gallary = new Galleries();
                    gallary.ImagePath = @"Images\Products\" + thumbnailName;
                    gallary.thumbnail = true;
                    gallary.displayOrder = 1;
                    product.Galleries.Add(gallary);
                    product.Thumbnail = @"Images\Products\" + thumbnailName;
                }
                if (imageUrls != null)
                {
                    string[] images = imageUrls.Split(',');
                    int counter = 1;
                    foreach(string image in images)
                    {
                        Galleries gallery = new Galleries();
                        gallery.ImagePath = image;
                        gallery.displayOrder = counter;
                        gallery.thumbnail = false;
                        product.Galleries.Add(gallery);
                        counter++;
                    }
                }
                if (Id == null)
                {
                    _unitOfWork.Product.Add(product);
                    _unitOfWork.Save();
                    TempData["Success"] = "Product Created Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    _unitOfWork.Product.Update(product);
                    _unitOfWork.Save();
                    TempData["Success"] = "Product Updated Successfully";
                    return RedirectToAction("Index");
                }
               
            }
            ViewData["User"] = await _userManager.GetUserAsync(User);
            return View();
        }

       

        #region APICALS
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            List<Product> products = _unitOfWork.Product.GetAll(IncludeProperties:"Category").ToList();
            return Json(new { data=products });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string Id)
        {
            Product product = _unitOfWork.Product.GetOrDefalut(x => x.Id == Id,
                IncludeProperties: "Galleries");
            if (product != null)
            {
                if (product.Galleries.Count > 0)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    foreach(var gallery in product.Galleries)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, gallery.ImagePath);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    
                }
                _unitOfWork.Product.Remove(product);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Product deleted successfully" });
            }
            else
            {
                return Json(new { success = false, message = "Error has happened" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> UploadImages(IFormFile file)
        {
            if (file != null)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, @"Images\Products");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return Json(new { filePath = @"Images\Products\"  + uniqueFileName });
            }

            return BadRequest("File upload failed");
        }

        #endregion
    }
}
