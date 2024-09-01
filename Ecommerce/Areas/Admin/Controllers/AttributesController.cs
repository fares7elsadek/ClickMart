using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using ClickMart.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClickMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AttributesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AttributesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Create(string productId)
        {
            IEnumerable<SelectListItem> Products = _unitOfWork.Product.GetAll().Select(x =>
                new SelectListItem
                {
                    Text = x.Title,
                    Value = x.Id,
                    Selected = x.Id == productId
                });
            ViewBag.Products = Products;
            ViewBag.productId = productId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Attributes attribute,string productId)
        {
            if (ModelState.IsValid)
            {
                var product = _unitOfWork.Product.GetOrDefalut(p => p.Id == productId);
                attribute.Products.Add(product);
                _unitOfWork.Attributes.Add(attribute);
                _unitOfWork.Save();
                TempData["Success"] = "Attribute Created Successfully";
                return RedirectToAction("Upsert","Product",new { Id=productId });
            }
            return View(attribute);
        }

        [HttpGet]
        public IActionResult Edit(string Id,string productId)
        {
            var attribute = _unitOfWork.Attributes.GetOrDefalut(a => a.Id == Id);
            IEnumerable<SelectListItem> products = _unitOfWork.Product.GetAll().Select(x =>
                    new SelectListItem
                    {
                        Text = x.Title,
                        Value = x.Id,
                        Selected = x.Id == productId
                    });
            ViewBag.Products = products;
            ViewBag.productId = productId;
            return View(attribute);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Attributes attribute,string productId)
        {
            if (ModelState.IsValid)
            {
                var product = _unitOfWork.Product.GetOrDefalut(p => p.Id == productId);
                attribute.Products.Add(product);
                _unitOfWork.Attributes.Update(attribute);
                _unitOfWork.Save();
                TempData["Success"] = "Attribute Updated Successfully";
                return RedirectToAction("Upsert", "Product", new { Id = productId });
            }
            return View(attribute);
        }

        #region APICALLS
        [HttpGet]
        public IActionResult GetAllAttributes(string Id)
        {
            var product = _unitOfWork.Product.GetOrDefalut( p => p.Id == Id,
                IncludeProperties: "Attributes");
            return Json(new { data = product.Attributes });
        }
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string Id)
        {
            Attributes attribute = _unitOfWork.Attributes.GetOrDefalut(x => x.Id == Id);
            if (attribute != null)
            {
                _unitOfWork.Attributes.Remove(attribute);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Attribute deleted successfully" });
            }
            else
            {
                return Json(new { success = false, message = "Error has happened" });
            }

        }
        #endregion
    }
}
