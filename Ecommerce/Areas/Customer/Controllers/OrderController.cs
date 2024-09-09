using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using ClickMart.Utility;
using ClickMart.ViewModels.Carts;
using ClickMart.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using NuGet.Protocol;
using Stripe.Checkout;
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
        [HttpGet]
        public IActionResult Index()
        {
            OrderHeader orderHeader = new OrderHeader();
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var carts = _unitOfWork.Cart.GetAllWithCondition(x => x.UserId == userId, IncludeProperties: "Product").ToList();
            if(carts.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            orderHeader.UserId = userId;
            decimal totalPrice = 0;
            decimal totalPriceAfterDiscount = 0;
            foreach (var cart in carts)
            {
                if (cart.Product == null) continue;
                totalPrice += (cart.Quantity * cart.Product.Price);
                totalPriceAfterDiscount += (cart.Quantity * cart.Product.DiscountPrice);
            }
            var shippingMethods = _unitOfWork.ShippingMethod.GetAll().ToList();
            var shippingMethod = shippingMethods.Where(x => x.Default).FirstOrDefault();
            var totalPriceAfterAddingShippingCost = totalPriceAfterDiscount + shippingMethod.Price;
            orderHeader.OrderTotal = totalPriceAfterAddingShippingCost;
            orderHeader.ShippingMethodId = shippingMethod.Id;
            orderHeader.AddressId = _unitOfWork.Address.GetOrDefalut(a => a.IsDefault &&
            a.UserId == userId).Id;
            var DiscountAmount = totalPrice - totalPriceAfterDiscount;
            OrderViewModel viewModel = new OrderViewModel();
            viewModel.ShippingCost = shippingMethod.Price;
            viewModel.FirstTotalPrice = totalPrice;
            viewModel.DiscountAmount = DiscountAmount;
            viewModel.Carts = carts;
            viewModel.ShippingMethods = shippingMethods;
            viewModel.Header = orderHeader;
            var Address = _unitOfWork.Address.GetAll(IncludeProperties:"Country").ToList();
            viewModel.AddressList = Address;
            return View(viewModel);
        }


        [HttpGet]
        public IActionResult IndexCoupon(string coupon)
        {
            OrderHeader orderHeader = new OrderHeader();
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var carts = _unitOfWork.Cart.GetAllWithCondition(x => x.UserId == userId, IncludeProperties: "Product").ToList();
            orderHeader.UserId = userId;
            decimal totalPrice = 0;
            decimal totalPriceAfterDiscount = 0;
            foreach (var cart in carts)
            {
                if (cart.Product == null) continue;
                totalPrice += (cart.Quantity * cart.Product.Price);
                totalPriceAfterDiscount += (cart.Quantity * cart.Product.DiscountPrice);
            }
            var shippingMethods = _unitOfWork.ShippingMethod.GetAll().ToList();
            var shippingMethod = shippingMethods.Where(x => x.Default).FirstOrDefault();
            var totalPriceAfterAddingShippingCost = totalPriceAfterDiscount + shippingMethod.Price;
            orderHeader.ShippingMethodId = shippingMethod.Id;
            orderHeader.AddressId = _unitOfWork.Address.GetOrDefalut(a => a.IsDefault &&
            a.UserId == userId).Id;
            var DiscountAmount = totalPrice - totalPriceAfterDiscount;
            OrderViewModel viewModel = new OrderViewModel();
            viewModel.ShippingCost = shippingMethod.Price;
            viewModel.FirstTotalPrice = totalPrice;
            viewModel.DiscountAmount = DiscountAmount;
            viewModel.Carts = carts;
            viewModel.ShippingMethods = shippingMethods;
            viewModel.Header = orderHeader;
            var Address = _unitOfWork.Address.GetAllWithCondition(x => x.UserId == userId, IncludeProperties: "Country").ToList();
            viewModel.AddressList = Address;
      
            decimal couponDiscount = 0;
            bool isCouponValid = false;
            var products = _unitOfWork.Product.GetAll(IncludeProperties: "Coupons").ToList();
            if (!string.IsNullOrEmpty(coupon))
            {
                isCouponValid = ValidateCoupon(coupon, products, ref couponDiscount);
            }
            decimal totalAfterCoupons = 0;
            decimal CouponDiscountAmount = 0;
            if (isCouponValid)
            {
                totalAfterCoupons = couponDiscount + shippingMethod.Price;
                CouponDiscountAmount =totalPriceAfterDiscount - couponDiscount;
                viewModel.CouponCode=coupon;
                viewModel.coupon = true;
                viewModel.Header.CouponId = _unitOfWork.Coupon.GetOrDefalut(x => x.code == coupon).Id;
            }
            else
            {
                TempData["Error"] = "Invalid coupon code";
                return RedirectToAction("Index");
            }
            viewModel.CouponDiscount = CouponDiscountAmount;
            viewModel.Header.OrderTotal= totalAfterCoupons;

            return View("Index",viewModel);
        }


        private bool ValidateCoupon(string coupon, List<Product> products, ref decimal couponDiscount)
        {
            bool valid = false;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var carts = _unitOfWork.Cart.GetAllWithCondition(x => x.UserId == userId,
                IncludeProperties: "Product");
            foreach (var cart in carts)
            {
                var productWithCoupons = products.FirstOrDefault(x => x.Id == cart.Product.Id);
                if (productWithCoupons != null && productWithCoupons.Coupons != null)
                {
                    var couponObj = productWithCoupons.Coupons.FirstOrDefault(c => c.code == coupon);
                    if (couponObj != null)
                    {
                       
                        couponDiscount += (cart.Quantity * cart.Product.DiscountPrice)
                                          * (1 - couponObj.discountValue / 100);
                        cart.TotalPriceAfterCoupon = (cart.Quantity * cart.Product.DiscountPrice)
                            - (cart.Quantity * cart.Product.DiscountPrice) * (couponObj.discountValue / 100);
                        valid = true;
                    }
                    else
                    {
                        couponDiscount += (cart.Quantity * cart.Product.DiscountPrice);
                    }
                }
            }
            if (valid)
            {
                _unitOfWork.Save();
                return true;
            }
            else
            {
                return false;
            }
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

            var currentDefaultShippingMethod = _unitOfWork.ShippingMethod.GetOrDefalut(x => x.Default);


            if (currentDefaultShippingMethod != null)
            {
                currentDefaultShippingMethod.Default = false;
            }


            var newDefaultShippingMethod = _unitOfWork.ShippingMethod.GetOrDefalut(x => x.Id == Id);

            if (newDefaultShippingMethod != null)
            {
                newDefaultShippingMethod.Default = true;
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
        public IActionResult OrderConfirmation(OrderHeader model)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var orderHeader = model;
                Random random = new Random();
                int sixDigitNumber = random.Next(100000, 1000000);
                orderHeader.TrackingNumber = sixDigitNumber.ToString();

                var carts = _unitOfWork.Cart.GetAllWithCondition(x => x.UserId == userId,IncludeProperties: "Product").ToList();
                if(carts.Count > 0)
                {
                    foreach (var cart in carts)
                    {
                        OrderDetails details = new OrderDetails();
                        details.ProductId = cart.ProductId;
                        details.qty = cart.Quantity;
                        details.Price = cart.Product.Price;
                        orderHeader.OrderDetails.Add(details);
                    }
                    var orderHeaderId = Guid.NewGuid().ToString();
                    orderHeader.Id = orderHeaderId;
                    _unitOfWork.OrderHeader.Add(orderHeader);
                    _unitOfWork.OrderHeader.UpdateStatus(orderHeaderId, SD.StatusPending, SD.StatusPending);
                    _unitOfWork.Save();
                    string domain = "http://localhost:5052/";
                    var options = new Stripe.Checkout.SessionCreateOptions
                    {
                        SuccessUrl = domain+ $"Customer/Order/OrderConfirmationPage/{orderHeaderId}",
                        CancelUrl = domain+ "Customer/Cart",
                        LineItems = new List<Stripe.Checkout.SessionLineItemOptions>(),
                        Mode = "payment",
                    };

                    foreach(var item in carts)
                    {
                        var sessionLineItem = new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = (long)(item.TotalPriceAfterCoupon!=null ?
                                item.TotalPriceAfterCoupon*100:item.Product.DiscountPrice*100),
                                Currency="usd",
                                ProductData = new SessionLineItemPriceDataProductDataOptions()
                                {
                                    Name = item.Product.Title
                                }
                            },
                            Quantity = item.Quantity
                        };
                        options.LineItems.Add(sessionLineItem);
                    }
                    var shipping = _unitOfWork.ShippingMethod.GetOrDefalut(x => x.Id
                    == orderHeader.ShippingMethodId);

                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(shipping.Price*100),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions()
                            {
                                Name = shipping.Name
                            }
                        },
                        Quantity = 1
                    });
                    var service = new Stripe.Checkout.SessionService();
                    Session session=service.Create(options);
                    _unitOfWork.OrderHeader.UpdateStripePaymentId(orderHeaderId,session.Id,session.PaymentIntentId);
                    _unitOfWork.Save();

                    Response.Headers.Add("Location", session.Url);
                    return new StatusCodeResult(303);
                }
                else
                {
                    TempData["Error"] = "The cart is empty";
                    return RedirectToAction("Index");   
                }
                
            }
            return RedirectToAction("Index");
        }

        public IActionResult OrderConfirmationPage(string Id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetOrDefalut(x => x.Id == Id
            , IncludeProperties: "User");
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ConfirmationPageViewModel viewModel = new ConfirmationPageViewModel();
            viewModel.PyamentDate = DateTime.Now;
            if (orderHeader != null)
            {
                if(orderHeader.UserId == userId)
                {
                    var service = new SessionService();
                    Session session = service.Get(orderHeader.SessionId);
                    if (session.PaymentStatus.ToLower() == "paid")
                    {
                        _unitOfWork.OrderHeader.UpdateStripePaymentId(Id, session.Id, session.PaymentIntentId);
                        _unitOfWork.OrderHeader.UpdateStatus(Id, SD.StatusApproved, SD.StatusApproved);
                        viewModel.OrderNumber = Id;
                        viewModel.PaymentMethod = "Stripe";
                        viewModel.TotalAmount = orderHeader.OrderTotal;
                        viewModel.IsConfirmed = true;

                        _unitOfWork.Cart.DeleteCart(orderHeader.UserId);
                        _unitOfWork.Save();
                        return View(viewModel);
                    }
                    else
                    {
                        _unitOfWork.OrderHeader.Remove(orderHeader);
                        _unitOfWork.Save();
                        viewModel.OrderNumber = Id;
                        viewModel.IsConfirmed = false;
                        viewModel.PaymentMethod = "Stripe";
                        return View(viewModel);
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            viewModel.OrderNumber = Id;
            viewModel.IsConfirmed = false;
            return View(viewModel);
        }


    }
}
