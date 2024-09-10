using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using ClickMart.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClickMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public OrderController(IUnitOfWork unitOfWork,
            UserManager<User> userManager) 
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["User"] = await _userManager.GetUserAsync(User);
            return View();
        }

        public async Task<IActionResult> Details(string Id)
        {
            var orderHeader = _unitOfWork.OrderHeader.GetOrDefalut(x =>
            x.Id == Id, IncludeProperties: "User,Address.Country,OrderDetails.Product");
            ViewData["User"] = await _userManager.GetUserAsync(User);
            return View(orderHeader);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChageOrderStatus(string Id,string Status)
        {
            switch(Status)
            {
                case SD.StatusApproved:
                    _unitOfWork.OrderHeader.UpdateStatus(Id, SD.StatusApproved);
                    TempData["Success"] = "Updated Successfully";
                    break;
                case SD.StatusInProcess:
                    _unitOfWork.OrderHeader.UpdateStatus(Id, SD.StatusInProcess);
                    TempData["Success"] = "Updated Successfully";
                    break;
                case SD.StatusShipped:
                    _unitOfWork.OrderHeader.UpdateStatus(Id, SD.StatusShipped);
                    TempData["Success"] = "Updated Successfully";
                    break;
                case SD.StatusDelevered:
                    _unitOfWork.OrderHeader.UpdateStatus(Id, SD.StatusDelevered);
                    TempData["Success"] = "Updated Successfully";
                    break;
                case SD.StatusCancelled:
                    _unitOfWork.OrderHeader.UpdateStatus(Id, SD.StatusCancelled);
                    TempData["Success"] = "Updated Successfully";
                    break;
                default :
                    TempData["Error"] = "Bad request";
                    break;
            }
            _unitOfWork.Save();
            return RedirectToAction("Details",new { Id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateShippingInfo(OrderHeader header)
        {
            _unitOfWork.OrderHeader.UpdateShippingInformation(header.Id, header.Carrier, header.TrackingNumber);
            _unitOfWork.Save();
            TempData["Success"] = "Updated Successfully";
            return RedirectToAction("Details", new { Id=header.Id });
        }
       
        #region APICALLS
        public IActionResult GetAllOrders(string? status)
        {
            List<OrderHeader> orders = new List<OrderHeader>();
            if (!string.IsNullOrWhiteSpace(status))
            {
                switch (status.ToLower())
                {
                    case "approved":
                       orders = _unitOfWork.OrderHeader.GetAllWithCondition(x => x.OrderStatus ==
                       SD.StatusApproved,IncludeProperties: "User").ToList()
                         .OrderByDescending(x => x.PaymentDate).ToList();
                        break;
                    case "processing":
                        orders = _unitOfWork.OrderHeader.GetAllWithCondition(x => x.OrderStatus ==
                       SD.StatusInProcess, IncludeProperties: "User").ToList()
                         .OrderByDescending(x => x.PaymentDate).ToList();
                        break;
                    case "pending":
                        orders = _unitOfWork.OrderHeader.GetAllWithCondition(x => x.OrderStatus ==
                       SD.StatusPending, IncludeProperties: "User").ToList()
                         .OrderByDescending(x => x.PaymentDate).ToList();
                        break;
                    case "shipped":
                        orders = _unitOfWork.OrderHeader.GetAllWithCondition(x => x.OrderStatus ==
                       SD.StatusShipped, IncludeProperties: "User").ToList()
                         .OrderByDescending(x => x.PaymentDate).ToList();
                        break;
                    case "delivered":
                        orders = _unitOfWork.OrderHeader.GetAllWithCondition(x => x.OrderStatus ==
                        SD.StatusDelevered, IncludeProperties: "User").ToList()
                          .OrderByDescending(x => x.PaymentDate).ToList();
                        break;
                    case "cancelled":
                        orders = _unitOfWork.OrderHeader.GetAllWithCondition(x => x.OrderStatus ==
                       SD.StatusCancelled, IncludeProperties: "User").ToList()
                         .OrderByDescending(x => x.PaymentDate).ToList();
                        break;
                    default:
                        orders = _unitOfWork.OrderHeader.GetAll(IncludeProperties: "User").ToList()
                         .OrderByDescending(x => x.PaymentDate).ToList();
                        break;
                }
                
            }
            return Json(new { data = orders });
        }
        #endregion
    }
}
