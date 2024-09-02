using Microsoft.AspNetCore.Mvc;
using ClickMart.DataAccess.Repository;
using ClickMart.Models.Models;
using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Utility;
using Microsoft.AspNetCore.Authorization;
using ClickMart.DataAccess.Migrations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace ClickMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public CompanyController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<Company> companies = _unitOfWork.Company.GetAll().ToList();
            ViewData["User"] = await _userManager.GetUserAsync(User);
            return View(companies);
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
                Company Company = _unitOfWork.Company.GetOrDefalut(x => x.Id == Id);
                return View(Company);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(string? Id,Company Company)
        {
            if (ModelState.IsValid)
            {
                if (Id == null)
                {
                    _unitOfWork.Company.Add(Company);
                    _unitOfWork.Save();
                    TempData["Success"] = "Company Created Successfully";
                }else 
                {
                    _unitOfWork.Company.Update(Company);
                    _unitOfWork.Save();
                    TempData["Success"] = "Company Updated Successfully";
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        

        #region APICALLS
        [HttpGet]
        public IActionResult GetAllCompanies()
        {
            List<Company> companies = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = companies });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string Id)
        {
            Company Company = _unitOfWork.Company.GetOrDefalut(x => x.Id == Id);
            if (Company != null)
            {
                _unitOfWork.Company.Remove(Company);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Company Deleted Successfully" });
            }
            else
            {
                return Json(new { success = false, message = "Error has happened" });
            }
        }
        #endregion
    }
}
