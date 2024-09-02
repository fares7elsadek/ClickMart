using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClickMart.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ReviewController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public ReviewController(IUnitOfWork unitOfWork,
            UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Reviews review)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var userData = _unitOfWork.Users.GetOrDefalut(u => u.Id == user.Id
                    ,IncludeProperties:"Reviews");
                    userData.Reviews.Add(review);
                    await _userManager.UpdateAsync(userData);
                }
                return RedirectToAction("Details", "Product", new { Id = review.ProductId });
            }
            return RedirectToAction("Details", "Product", new { Id = review.ProductId });
        }
    }
}
