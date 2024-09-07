using ClickMart.DataAccess.Repository;
using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using ClickMart.ViewModels.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using System;
using System.Security.Claims;

namespace ClickMart.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var carts = _unitOfWork.Cart.GetAllWithCondition(x=>x.UserId == userId,
                IncludeProperties:"Product").ToList();
            var productIds = carts.Select(x => x.ProductId).ToList();
            var productWithCategories = _unitOfWork.Product
                .GetAllWithCondition(x => productIds.Contains(x.Id),
                IncludeProperties: "Category").ToDictionary(p => p.Id);

            decimal totalPrice = 0;
            decimal DiscountPrice = 0;
            HashSet<string> categories = new HashSet<string>();
            List<Product> RecommenedList = new List<Product>();
            foreach (var cart in carts)
            {
                var product = productWithCategories[cart.ProductId];
                totalPrice += (cart.Quantity * cart.Product.Price);
                DiscountPrice += (cart.Quantity * cart.Product.DiscountPrice);
                categories.Add(product.Category.Id);
            }
            var random = new Random();
            foreach (var category in categories)
            {
                var products = _unitOfWork.Category.GetOrDefalut(x => x.Id == category
                ,IncludeProperties:"Products")
                    .Products.OrderBy(x => random.Next()).Take(5).ToList();

                RecommenedList.AddRange(products);
            }
            decimal Discount = totalPrice - DiscountPrice;
            CartViewModel viewModel = new CartViewModel();
            viewModel.RecommendedProducts = RecommenedList.OrderBy(x => random.Next()).Take(4).ToList();
            viewModel.TotalPrice = totalPrice;
            viewModel.TotalDiscount = DiscountPrice;
            viewModel.Discount = Discount;
            viewModel.Carts = carts;

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Plus(string Id)
        {
            var cart = _unitOfWork.Cart.GetOrDefalut(c => c.Id == Id);
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == cart.UserId)
            {
                cart.Quantity += 1;
                _unitOfWork.Save();
                TempData["Success"] = "Done";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Minus(string Id)
        {
            var cart = _unitOfWork.Cart.GetOrDefalut(c => c.Id == Id);
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            if(userId== cart.UserId && cart.Quantity - 1 >0)
            {
                cart.Quantity -= 1;
                _unitOfWork.Save();
                TempData["Success"] = "Done";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(string Id)
        {
			var cart = _unitOfWork.Cart.GetOrDefalut(c => c.Id == Id);
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			if (userId == cart.UserId)
			{
				_unitOfWork.Cart.Remove(cart);
				_unitOfWork.Save();
                TempData["Success"] = "Done";
            }
			return RedirectToAction("Index");
		}
    }
}
