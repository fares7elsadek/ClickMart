using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using ClickMart.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClickMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public OrderController(IUnitOfWork unitOfWork,
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

        #region APICALLS
        public IActionResult GetAllOrders()
        {
            var orders = _unitOfWork.OrderHeader.GetAll(IncludeProperties: "User").ToList();
            return Json(new { data = orders });
        }
        #endregion
    }
}
