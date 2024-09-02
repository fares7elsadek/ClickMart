using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using ClickMart.ViewModels.product;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Details(string Id)
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
    }
}
