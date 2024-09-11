using ClickMart.DataAccess.Repository;
using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using ClickMart.Utility;
using ClickMart.ViewModels.Carts;
using ClickMart.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using NuGet.Protocol;
using Stripe;
using Stripe.Checkout;
using Stripe.Climate;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClickMart.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public OrderController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var carts = _unitOfWork.Cart.GetAllWithCondition(x => x.UserId == userId, IncludeProperties: "Product").ToList();
            if (carts.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var orderHeader = new OrderHeader { UserId = userId };
            var (totalPrice, totalPriceAfterDiscount) = CalculateOrderTotals(carts);

            var shippingMethods = _unitOfWork.ShippingMethod.GetAll().ToList();

            var user = _unitOfWork.Users.GetOrDefalut(x => x.Id == userId);
            ShippingMethod shippingMethod;
            if (string.IsNullOrEmpty(user.shippingMethodId))
            {
                shippingMethod = shippingMethods.FirstOrDefault(x => x.Default);
            }
            else
            {
                shippingMethod = shippingMethods.FirstOrDefault(x => x.Id==user.shippingMethodId);
            }

            if (shippingMethod == null)
            {
                ModelState.AddModelError(string.Empty, "No default shipping method found.");
                return View();
            }

            orderHeader.OrderTotal = totalPriceAfterDiscount + shippingMethod.Price;
            orderHeader.ShippingMethodId = shippingMethod.Id;

            var addresses = _unitOfWork.Address.GetAllWithCondition(x => x.UserId == userId, IncludeProperties: "Country").ToList();
            var defaultAddress = addresses.FirstOrDefault(a => a.IsDefault);

            if (defaultAddress != null)
            {
                orderHeader.AddressId = defaultAddress.Id;
            }

            var viewModel = new OrderViewModel
            {
                ShippingCost = shippingMethod.Price,
                FirstTotalPrice = totalPrice,
                DiscountAmount = totalPrice - totalPriceAfterDiscount,
                Carts = carts,
                ShippingMethods = shippingMethods,
                Header = orderHeader,
                AddressList = addresses
            };

            return View(viewModel);
        }

        private (decimal totalPrice, decimal totalPriceAfterDiscount) CalculateOrderTotals(IEnumerable<Cart> carts)
        {
            decimal totalPrice = 0, totalPriceAfterDiscount = 0;
            foreach (var cart in carts)
            {
                if (cart.Product == null) continue;
                totalPrice += cart.Quantity * cart.Product.Price;
                totalPriceAfterDiscount += cart.Quantity * cart.Product.DiscountPrice;
            }
            return (totalPrice, totalPriceAfterDiscount);
        }


        [HttpGet]
        public IActionResult IndexCoupon(string coupon)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var carts = _unitOfWork.Cart.GetAllWithCondition(x => x.UserId == userId, IncludeProperties: "Product.Coupons").ToList();
            if (carts.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var orderHeader = new OrderHeader { UserId = userId };
            var (totalPrice, totalPriceAfterDiscount) = CalculateOrderTotals(carts);

            var shippingMethods = _unitOfWork.ShippingMethod.GetAll().ToList();
            var user = _unitOfWork.Users.GetOrDefalut(x => x.Id == userId);
            ShippingMethod shippingMethod;
            if (string.IsNullOrEmpty(user.shippingMethodId))
            {
                shippingMethod = shippingMethods.FirstOrDefault(x => x.Default);
            }
            else
            {
                shippingMethod = shippingMethods.FirstOrDefault(x => x.Id == user.shippingMethodId);
            }

            if (shippingMethod == null)
            {
                ModelState.AddModelError(string.Empty, "No default shipping method found.");
                return View();
            }

            orderHeader.OrderTotal = totalPriceAfterDiscount + shippingMethod.Price;
            orderHeader.ShippingMethodId = shippingMethod.Id;

            var addresses = _unitOfWork.Address.GetAllWithCondition(x => x.UserId == userId, IncludeProperties: "Country").ToList();
            var defaultAddress = addresses.FirstOrDefault(a => a.IsDefault);
            if (defaultAddress != null)
            {
                orderHeader.AddressId = defaultAddress.Id;
            }

            var products = _unitOfWork.Product.GetAll(IncludeProperties: "Coupons").ToList();
            decimal couponDiscount = 0;
            bool isCouponValid = !string.IsNullOrEmpty(coupon) && ValidateCoupon(coupon, carts, ref couponDiscount);

            if (!isCouponValid)
            {
                TempData["Error"] = "Invalid coupon code";
                return RedirectToAction("Index");
            }

            var totalAfterCoupons = totalPriceAfterDiscount - couponDiscount + shippingMethod.Price;
            orderHeader.OrderTotal = totalAfterCoupons;

            var viewModel = new OrderViewModel
            {
                ShippingCost = shippingMethod.Price,
                FirstTotalPrice = totalPrice,
                DiscountAmount = totalPrice - totalPriceAfterDiscount,
                CouponCode = coupon,
                CouponDiscount = couponDiscount,
                Header = orderHeader,
                Carts = carts,
                ShippingMethods = shippingMethods,
                AddressList = addresses,
                coupon = true
            };

            return View("Index", viewModel);
        }


        private bool ValidateCoupon(string coupon, List<Cart> carts, ref decimal couponDiscount)
        {
            var valid = false;
            foreach (var cart in carts)
            {
                var productCoupons = cart.Product.Coupons;
                var couponObj = productCoupons?.FirstOrDefault(c => c.code == coupon);

                if (couponObj != null)
                {
                    couponDiscount += (cart.Quantity * cart.Product.DiscountPrice) * (couponObj.discountValue / 100);
                    cart.TotalPriceAfterCoupon = (cart.Quantity * cart.Product.DiscountPrice) -
                                                  (cart.Quantity * cart.Product.DiscountPrice) * (couponObj.discountValue / 100);
                    valid = true;
                }
            }

            if (valid)
            {
                _unitOfWork.Save();
            }
            return valid;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeAddressStatus(string Id, string? Coupon)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var currentDefaultAddress = _unitOfWork.Address.GetOrDefalut(x => x.IsDefault
            && x.UserId == userId);

            
            if (currentDefaultAddress != null)
            {
                currentDefaultAddress.IsDefault = false;
            }

           
            var newDefaultAddress = _unitOfWork.Address.GetOrDefalut(x => x.Id == Id&&
            x.UserId == userId);

            if (newDefaultAddress != null)
            {
                newDefaultAddress.IsDefault = true; 
            }

            
            _unitOfWork.Save();

            
            if (string.IsNullOrEmpty(Coupon))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("IndexCoupon", new { coupon = Coupon });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeShippingMethod(string Id, string? Coupon)
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }


            var newDefaultShippingMethod = _unitOfWork.ShippingMethod.GetOrDefalut(x => x.Id == Id);

            if (newDefaultShippingMethod != null)
            {
                var user = _unitOfWork.Users.GetOrDefalut(x => x.Id == userId);
                if(user.shippingMethodId != Id)
                {
                    user.shippingMethodId = Id;
                }
            }

            _unitOfWork.Save();

            if (string.IsNullOrEmpty(Coupon))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("IndexCoupon", new { coupon = Coupon });
            }
        }

        private string GenerateInvoiceNumber()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder result = new StringBuilder("#");

            Random random = new Random();
            for (int i = 0; i < 7; i++) // 7 characters after '#'
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OrderConfirmation(OrderHeader model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                TempData["Error"] = string.Join("; ", errors);
                if (!string.IsNullOrEmpty(model.CouponId))
                {
                    var coupon = _unitOfWork.Coupon.GetOrDefalut(x => x.Id==model.CouponId).code;
                    return RedirectToAction("IndexCoupon",new {coupon});
                }
                return RedirectToAction("Index");
            }

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var carts = _unitOfWork.Cart.GetAllWithCondition(x => x.UserId == userId, IncludeProperties: "Product").ToList();
            if (carts.Count == 0)
            {
                TempData["Error"] = "The cart is empty";
                return RedirectToAction("Index");
            }

            var orderHeader = model;
            orderHeader.TrackingNumber = Guid.NewGuid().ToString();

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    foreach (var cart in carts)
                    {
                        var details = new OrderDetails
                        {
                            ProductId = cart.ProductId,
                            qty = cart.Quantity,
                            Price = cart.Product.Price
                        };
                        orderHeader.OrderDetails.Add(details);
                    }

                    var orderHeaderId = Guid.NewGuid().ToString();
                    orderHeader.Id = orderHeaderId;
                    orderHeader.Invoice = GenerateInvoiceNumber();
                    _unitOfWork.OrderHeader.Add(orderHeader);
                    _unitOfWork.OrderHeader.UpdateStatus(orderHeaderId, SD.StatusPending, SD.StatusPending);
                    _unitOfWork.Save();

                    
                    string domain = _configuration["Stripe:RedirectDomain"];

                    var options = new Stripe.Checkout.SessionCreateOptions
                    {
                        SuccessUrl = domain + $"Customer/Order/OrderConfirmationPage/{orderHeaderId}",
                        CancelUrl = domain + "Customer/Cart",
                        LineItems = carts.Select(cart => new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = (long)((cart.TotalPriceAfterCoupon ?? cart.Product.DiscountPrice) * 100),
                                Currency = "usd",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = cart.Product.Title
                                }
                            },
                            Quantity = cart.Quantity
                        }).ToList(),
                        Mode = "payment"
                    };

                    
                    var shipping = _unitOfWork.ShippingMethod.GetOrDefalut(x => x.Id == orderHeader.ShippingMethodId);
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(shipping.Price * 100),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = shipping.Name
                            }
                        },
                        Quantity = 1
                    });

                    var service = new Stripe.Checkout.SessionService();
                    var session = service.Create(options);
                    _unitOfWork.OrderHeader.UpdateStripePaymentId(orderHeaderId, session.Id, session.PaymentIntentId);
                    _unitOfWork.Save();

                    transaction.Commit();

                    Response.Headers.Add("Location", session.Url);
                    return new StatusCodeResult(303);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    TempData["Error"] = "An error occurred. Please try again.";
                    return RedirectToAction("Index");
                }
            }
        }

        public IActionResult OrderConfirmationPage(string Id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetOrDefalut(x => x.Id == Id);

            if (orderHeader == null || orderHeader.UserId != userId)
            {
                return Unauthorized();
            }

            ConfirmationPageViewModel viewModel = new ConfirmationPageViewModel
            {
                PyamentDate = DateTime.Now,
                OrderNumber = Id,
                PaymentMethod = "Stripe",
                IsConfirmed = false
            };

            try
            {
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);

                if (session?.PaymentStatus?.ToLower() == "paid")
                {
                    _unitOfWork.OrderHeader.UpdateStripePaymentId(Id, session.Id, session.PaymentIntentId);
                    _unitOfWork.OrderHeader.UpdateStatus(Id, SD.StatusApproved, SD.StatusApproved);

                    viewModel.IsConfirmed = true;
                    viewModel.TotalAmount = orderHeader.OrderTotal;

                    _unitOfWork.Cart.DeleteCart(orderHeader.UserId);
                    _unitOfWork.Save();
                }
                else
                {
                    _unitOfWork.OrderHeader.Remove(orderHeader);
                    _unitOfWork.Save();
                }
            }
            catch (StripeException ex)
            {
                TempData["Error"] = "There was an issue retrieving payment information. Please try again.";
                return RedirectToAction("ErrorPage");
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Orders(int page = 1)
        {
            if (page <= 0)
            {
                page = 1;
            }
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var orders = _unitOfWork.OrderHeader.GetAllWithCondition(
                    o => o.UserId == userId && o.OrderStatus != SD.StatusCancelled,
                    IncludeProperties: "OrderDetails.Product,User,Address.Country,ShippingMethod"
             ).OrderByDescending(o => o.OrderDate).ToList();

            int total = orders.Count();
            int size = 2;
            int pages = (int)Math.Ceiling((decimal)total / size);
            if (page > pages)
            {
                page = pages;
            }
            TempData["pages"] = pages;
            var result = orders.Skip((page - 1) * size).Take(size).ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult Tracking(string Id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var orderHeader = _unitOfWork.OrderHeader.GetOrDefalut(o => o.Id==Id
            && o.OrderStatus!=SD.StatusCancelled,IncludeProperties: "ShippingMethod");
            if(orderHeader == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if(orderHeader.UserId !=userId)
            {
                return Unauthorized();
            }
            int numberOfDays = 0;
            string shippingMethodName = orderHeader.ShippingMethod.Name;
            switch (shippingMethodName)
            {
                case "Standard Shipping":
                    numberOfDays = 6; break;
                case "Express Shipping":
                    numberOfDays = 3; break;
                case "Overnight Shipping":
                    numberOfDays = 1; break;
            }
            DateTime orderDate = (DateTime)orderHeader.OrderDate;
            var ExpectedDate = DateOnly.FromDateTime(orderDate.AddDays(numberOfDays));
            TempData["ExpectedDate"] = ExpectedDate;
            return View(orderHeader);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelOrder(string Id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var order = _unitOfWork.OrderHeader.GetOrDefalut(o => o.Id == Id);
            if(order == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if(userId != order.UserId)
            {
                return Unauthorized();
            }
            if(order.OrderStatus == SD.StatusApproved)
            {
                order.OrderStatus = SD.StatusCancelled;
                _unitOfWork.Save();
            }
            else
            {
                TempData["Error"] = "You can't cancele the order at this phase";
            }

           
            return LocalRedirect("/Identity/Account/Manage");
        }

    }
}
