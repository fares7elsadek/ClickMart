using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.ViewModels.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClickMart.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var carts = _unitOfWork.Cart.GetAllWithCondition(x => x.UserId == userId,
                IncludeProperties: "Product").ToList();
            decimal totalPrice = 0;
            decimal discountPrice = 0;
            foreach(var cart in carts)
            {
                totalPrice += (cart.Quantity * cart.Product.Price);
                discountPrice += (cart.Quantity * cart.Product.DiscountPrice);
            }
            decimal Discount = totalPrice - discountPrice;
            CartViewModel viewModel = new CartViewModel();
            viewModel.TotalPrice = totalPrice;
            viewModel.TotalDiscount = discountPrice;
            viewModel.Discount = Discount;
            viewModel.Carts = carts;
            return View(viewModel);
        }
    }
}
