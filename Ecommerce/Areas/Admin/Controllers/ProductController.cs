using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;


namespace ClickMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.Product.GetAll(IncludeProperties:"Category").ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Upsert(string? Id)
        {
            IEnumerable<SelectListItem> Categories = _unitOfWork.Category.GetAll().Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id,
                });
            ViewBag.Categories = Categories;
            if (Id == null)
            {
                return View();
            }
            else
            {
                Product product = _unitOfWork.Product.GetOrDefalut(x => x.Id == Id);
                return View(product);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(string? Id,Product product,IFormFile? ImageFile)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (ImageFile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"Images\Products");
                    if (!Directory.Exists(productPath))
                    {
                        Directory.CreateDirectory(productPath);
                    }
                    var existingProduct = product;
                    if (existingProduct != null && existingProduct.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, existingProduct.ImageUrl);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        ImageFile.CopyTo(fileStream);
                    }
                    product.ImageUrl = @"Images\Products\" + fileName;
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
            Product product = _unitOfWork.Product.GetOrDefalut(x => x.Id == Id);
            if (product != null)
            {
                if (product.ImageUrl != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    var oldImagePath = Path.Combine(wwwRootPath, product.ImageUrl);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
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
        #endregion
    }
}
