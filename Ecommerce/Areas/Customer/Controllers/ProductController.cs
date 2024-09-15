using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using ClickMart.Utility;
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
            var product = _unitOfWork.Product.GetOrDefalut(p => p.Id == Id, IncludeProperties: "Category,Attributes,Galleries");
            List<Product> SameCategoryProducts = _unitOfWork.Product.GetAllWithCondition(p => (p.CategoryId == product.CategoryId && p.Id !=product.Id)).ToList();
            var reviews = _unitOfWork.Reviews.GetAllWithCondition(r => r.ProductId == product.Id,
                 IncludeProperties: "User").ToList();

            if (User.Identity.IsAuthenticated)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = _unitOfWork.Users.GetOrDefalut(u => u.Id == userId,
                    IncludeProperties:"Products");
                var IsExist = user.Products.Contains(product);
                if (!IsExist)
                {
                    user.Products.Add(product);
                    _unitOfWork.Save();
                }
            }

            
            ProductDeatailsViewModel viewModel = new ProductDeatailsViewModel();
            viewModel.product = product;
            viewModel.SameCategoryProducts = SameCategoryProducts;
            viewModel.reviews = reviews;
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Search(string? categoryIds,bool? offer=false, string s = "", int page = 1)
        {
            if (string.IsNullOrEmpty(s) && string.IsNullOrEmpty(categoryIds) && offer == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (page <= 0)
            {
                page = 1;
            }

            List<Product> Products;
            List<string> selectedCategories = new List<string>();


            if (!string.IsNullOrEmpty(categoryIds))
            {
                var categoryIdList = categoryIds.Split(',').ToList();
                selectedCategories = categoryIdList;
                Products = _unitOfWork.Product.GetAllWithCondition(p =>
                    categoryIdList.Contains(p.CategoryId) && p.Title.Contains(s)
                    ).ToList();
            }
            else if (offer != null)
            {
                Products = _unitOfWork.Product.GetAllWithCondition(p => p.OnSale == offer).ToList();
            }
            else
            {
              
                Products = _unitOfWork.Product.GetAllWithCondition(p => p.Title.Contains(s)).ToList();
            }

            int total = Products.Count();
            int size = 5;
            int pages = (int)Math.Ceiling((decimal)total / size);
            if (page > pages)
            {
                page = pages;
            }

            var result = Products.Skip((page - 1) * size).Take(size).ToList();
            var categories = _unitOfWork.Category.GetAll().ToList();

          
            ProductSearchViewModel viewModel = new ProductSearchViewModel
            {
                products = result,
                TotalProducts = total,
                NumberOfPages = pages,
                SerachString = s,
                CurrentPage = page,
                categories = categories,
                SelectedCategories = selectedCategories.Select(id => id.ToString()).ToList()
            };

            return View(viewModel);
        }


        #region APICALLS
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
            HttpContext.Session.SetInt32(SD.CartCounter,
                    _unitOfWork.Cart.GetAllWithCondition(o => o.UserId == userId).ToList().Count);
            return Json(new { success = true, message = "Product Added to the cart successfully" });
        }
        [HttpGet]
        public IActionResult GetProductSuggestions(string searchString)
        {
            var suggestions = _unitOfWork.Product
                .GetAllWithCondition(p => p.Title.StartsWith(searchString))
                .Select(p => p.Title)
                .ToList();

            return Json(suggestions);
        }
        #endregion
    }
}
