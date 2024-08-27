using ClickMart.DataAccess.Repository.IRepository;
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
            return View(_unitOfWork.Product.GetOrDefalut(p => p.Id==Id));
        }
    }
}
