using Microsoft.AspNetCore.Mvc;
using ClickMart.DataAccess.Repository;
using ClickMart.Models.Models;
using ClickMart.DataAccess.Repository.IRepository;

namespace ClickMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Upsert(string? Id)
        {
            if(Id == null)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string Id)
        {
            Category category = _unitOfWork.Category.GetOrDefalut(x => x.Id == Id);
            if (category != null)
            {
                _unitOfWork.Category.Remove(category);
                _unitOfWork.Save();
                TempData["Success"] = "Category Deleted Successfully";
            }
            else
            {
                TempData["Error"] = "Category Not Found";
            }

            return RedirectToAction("Index");
        }
    }
}
