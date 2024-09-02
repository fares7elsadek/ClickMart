using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using ClickMart.Utility;
using ClickMart.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClickMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AdminPanelController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public AdminPanelController(UserManager<User> userManager
            ,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
       
        public async Task<IActionResult> Dashboard()
        {
            List<Product> products = _unitOfWork.Product.GetAll(IncludeProperties: "Category").ToList();
            List<Category> Categories = _unitOfWork.Category.GetAll().ToList();
            //orders
            var Users = _unitOfWork.Users.GetAll().ToList();
            var totalCustomers = Users.Count();
            var user = await _userManager.GetUserAsync(User);
            AdminDashboardViewModel adminDashboard = new AdminDashboardViewModel
            {
                Products = products,
                Categories = Categories,
                Users = Users,
                TotalCustomers = totalCustomers,
                User = user
            };
            
            return View(adminDashboard);
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
                    x.avatar
                })
                .ToList();

            return Json(new { data = users });
        }
        #endregion
    }
}
