using ClickMart.Models.Models;

namespace ClickMart.ViewModels.Order
{
    public class OrderViewModel
    {
        public List<Cart> Carts { get; set; }
        public List<ShippingMethod> ShippingMethods { get; set; }

        public OrderHeader Header { get; set; }
        public decimal ShippingCost { get; set; }

        public decimal FirstTotalPrice { get; set; }

        public decimal DiscountAmount { get; set; }

        public List<Address> AddressList { get; set; }

        public decimal CouponDiscount {  get; set; }

        public bool coupon { get; set; }

        public string CouponCode { get; set; }

    }
}
