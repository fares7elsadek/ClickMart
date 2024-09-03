using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using ClickMart.ViewModels.product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClickMart.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;    
        public ProductController(IUnitOfWork unitOfWork
            ,UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Details(string Id)
        {
            var product = _unitOfWork.Product.GetOrDefalut(p => p.Id == Id, IncludeProperties: "Category,Attributes");
            List<Product> SameCategoryProducts = _unitOfWork.Product.GetAllWithCondition(p => (p.CategoryId == product.CategoryId && p.Id !=product.Id)).ToList();
            var reviews = _unitOfWork.Reviews.GetAllWithCondition(r => r.ProductId == product.Id,
                 IncludeProperties:"User").ToList();
            ProductDeatailsViewModel viewModel = new ProductDeatailsViewModel();
            viewModel.product = product;
            viewModel.SameCategoryProducts = SameCategoryProducts;
            viewModel.reviews = reviews;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult AddToCart(Cart cart,string? place)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var ProductExsit = _unitOfWork.Cart.GetOrDefalut(x => x.ProductId == cart.ProductId
            && x.UserId == userId);
            if (ProductExsit != null)
            {
                ProductExsit.Quantity += cart.Quantity;
            }
            else
            {
                cart.UserId = userId;
                _unitOfWork.Cart.Add(cart);
            }
            _unitOfWork.Save();
            //if (!string.IsNullOrEmpty(place)&& place=="home")
            //{
            //    return RedirectToAction("Index", "Home");
            //}else if(!string.IsNullOrEmpty(place) && place == "Cart")
            //{
            //    return RedirectToAction("Index", "Cart");
            //}
            return Json(new { success = true, message = "Product Added to the cart successfully" });
            //return RedirectToAction("Details",new { Id = cart.ProductId });   
        }
    }
}
