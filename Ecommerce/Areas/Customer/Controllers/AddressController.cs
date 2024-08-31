using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClickMart.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AddressController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public AddressController(IUnitOfWork unitOfWork
            ,UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Add()
        {
            IEnumerable<SelectListItem> Categories = _unitOfWork.Countries.GetAll().Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id,
                });
            ViewData["Countries"] = Categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Address address)
        {
            if (!ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                UserAddress userAddress = new UserAddress()
                {
                    Address = address,
                    User = user
                };
                user.Address.Add(userAddress);
                await _userManager.UpdateAsync(user);
                return LocalRedirect("/Identity/Account/Manage");
            }
            return View();
        }
    }
}
