using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using ClickMart.ViewModels.product;
using Microsoft.AspNetCore.Mvc;

namespace ClickMart.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Details(string Id)
        {
            var product = _unitOfWork.Product.GetOrDefalut(p => p.Id == Id, IncludeProperties: "Category,Attributes");
            List<Product> SameCategoryProducts = _unitOfWork.Product.GetAllWithCondition(p => (p.CategoryId == product.CategoryId && p.Id !=product.Id)).ToList();
            ProductDeatailsViewModel viewModel = new ProductDeatailsViewModel();
            viewModel.product = product;
            viewModel.SameCategoryProducts = SameCategoryProducts;
            return View(viewModel);
        }
    }
}
