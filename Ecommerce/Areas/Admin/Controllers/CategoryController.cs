using Microsoft.AspNetCore.Mvc;
using ClickMart.DataAccess.Repository;
using ClickMart.Models.Models;
using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace ClickMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public CategoryController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
            ViewData["User"] = await _userManager.GetUserAsync(User);
            return View(categories);
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
                Category category = _unitOfWork.Category.GetOrDefalut(x => x.Id == Id);
                return View(category);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(string? Id,Category category)
        {
            if (ModelState.IsValid)
            {
                if (Id == null)
                {
                    _unitOfWork.Category.Add(category);
                    _unitOfWork.Save();
                    TempData["Success"] = "Category Created Successfully";
                }else 
                {
                    _unitOfWork.Category.Update(category);
                    _unitOfWork.Save();
                    TempData["Success"] = "Category Updated Successfully";
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        

        #region APICALLS
        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var Categories = _unitOfWork.Category.GetAll().ToList();
            return Json(new { data = Categories });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string Id)
        {
            Category category = _unitOfWork.Category.GetOrDefalut(x => x.Id == Id);
            if (category != null)
            {
                _unitOfWork.Category.Remove(category);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Category Deleted Successfully" });
            }
            else
            {
                return Json(new { success = false, message = "Error has happened" });
            }
        }
        #endregion
    }
}
