using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClickMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AttributesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AttributesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Upsert(string? Id,string? productId)
        {
            IEnumerable<SelectListItem> Products = _unitOfWork.Product.GetAll().Select(x =>
                new SelectListItem
                {
                    Text = x.Title,
                    Value = x.Id,
                    Selected = x.Id == productId
                });
            ViewBag.Products = Products;
            if (!string.IsNullOrEmpty(productId))
            {
                ViewBag.ProductId = productId;
            }
            if (Id == null)
            {
                return View();
            }
            else
            {
                var Attribute = _unitOfWork.Attributes.GetOrDefalut(a => a.Id == Id);
                return View(Attribute);
            }
        }

        [HttpPost]
        public IActionResult Upsert(string? Id,Attributes attributes)
        {
            if (ModelState.IsValid)
            {
                if (Id == null)
                {
                    _unitOfWork.Attributes.Add(attributes);
                    _unitOfWork.Save();
                    TempData["Success"] = "Attribute Created Successfully";
                    return RedirectToAction("Upsert", "Product");
                }
                else
                {
                    _unitOfWork.Attributes.Update(attributes);
                    _unitOfWork.Save();
                    TempData["Success"] = "Attribute Updated Successfully";
                    return RedirectToAction("Upsert", "Product", new { Id });
                }
            }
            if(Id == null)
            {
                return RedirectToAction("Upsert","Product");
            }
            else
            {
                return RedirectToAction("Upsert", "Product", new { Id });
            }
           
        }

        #region APICALLS
        [HttpGet]
        public IActionResult GetAllAttributes()
        {
            var Attributes = _unitOfWork.Attributes.GetAll().ToList();
            return Json(new { data = Attributes });
        }

        #endregion
    }
}
