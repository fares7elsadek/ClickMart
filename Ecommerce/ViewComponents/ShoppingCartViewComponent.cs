using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClickMart.ViewCompenents
{
    public class ShoppingCartViewComponent: ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            if(userId != null)
            {
                if (HttpContext.Session.GetInt32(SD.CartCounter) != null)
                {
                    HttpContext.Session.SetInt32(SD.CartCounter,
                    _unitOfWork.Cart.GetAllWithCondition(o => o.UserId == userId).ToList().Count);
                    
                }
                return View(HttpContext.Session.GetInt32(SD.CartCounter));
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
