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
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public UserController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<User> companies = _unitOfWork.Users.GetAll().ToList();
            ViewData["User"] = await _userManager.GetUserAsync(User);
            return View(companies);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? Id)
        {
            User user = _unitOfWork.Users.GetOrDefalut(x => x.Id == Id);
            ViewData["User"]= await _userManager.GetUserAsync(User);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Id,User user)
        {
            if (ModelState.IsValid)
            {
                await _userManager.UpdateAsync(user);
                TempData["Success"] = "User Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }



        #region APICALLS
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _unitOfWork.Users.GetAll()
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.UserName,
                    x.Email,
                    x.avatar,
                    x.PhoneNumber,
                    role = _userManager.GetRolesAsync(x).Result[0]
                    
                })
                .ToList();

            return Json(new { data = users });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                return Json(new { success = true, message = "User Deleted Successfully" });
            }
            else
            {
                return Json(new { success = false, message = "Error has happened" });
            }
        }
        #endregion
    }
}
