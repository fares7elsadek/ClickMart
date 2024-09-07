using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using ClickMart.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClickMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CouponsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public CouponsController(IUnitOfWork unitOfWork,
            UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            
            ViewData["User"] = await _userManager.GetUserAsync(User);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(string? Id)
        {
            ViewData["User"] = await _userManager.GetUserAsync(User);
            if (Id == null)
            {
                return View();
            }
            else
            {
                Coupons coupon = _unitOfWork.Coupon.GetOrDefalut(x => x.Id == Id);
                return View(coupon);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(string? Id, Coupons coupon)
        {
            if (ModelState.IsValid)
            {
                if (Id == null)
                {
                    _unitOfWork.Coupon.Add(coupon);
                    _unitOfWork.Save();
                    TempData["Success"] = "Coupon Created Successfully";
                }
                else
                {
                    _unitOfWork.Coupon.Update(coupon);
                    _unitOfWork.Save();
                    TempData["Success"] = "Coupon Updated Successfully";
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddToProduct(string productId)
        {
            IEnumerable<SelectListItem> coupons = _unitOfWork.Coupon.GetAll().Select(x =>
               new SelectListItem
               {
                   Text = x.code,
                   Value = x.Id,
               });
            ViewData["User"] = await _userManager.GetUserAsync(User);
            ViewBag.Coupon = coupons;
            ViewBag.ProductId = productId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToProduct(string productId, string couponId)
        {
           
            var product = _unitOfWork.Product.GetOrDefalut(p => p.Id == productId, IncludeProperties: "Coupons");

            if (product != null)
            {
                
                var coupon = _unitOfWork.Coupon.GetOrDefalut(c => c.Id == couponId);

                if (coupon != null)
                {

                    if (!product.Coupons.Any(c => c.Id == couponId))
                    {
                        product.Coupons.Add(coupon);
                        TempData["Success"] = "Coupon added successfully";
                        _unitOfWork.Save();
                    }
                    else
                    {
                        TempData["Warning"] = "Coupon is already added to this product.";
                    }
                }
                else
                {
                    TempData["Error"] = "Coupon not found.";
                }
            }
            else
            {
                TempData["Error"] = "Product not found.";
            }

            return RedirectToAction("Index", "Product");
        }




        #region APICALLS
        [HttpGet]
        public IActionResult GetAllCoupons()
        {
            var Coupons = _unitOfWork.Coupon.GetAll().ToList();
            return Json(new { data = Coupons });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string Id)
        {
            Coupons coupons = _unitOfWork.Coupon.GetOrDefalut(x => x.Id == Id);
            if (coupons != null)
            {
                _unitOfWork.Coupon.Remove(coupons);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Coupon Deleted Successfully" });
            }
            else
            {
                return Json(new { success = false, message = "Error has happened" });
            }
        }
        #endregion
    }
}
