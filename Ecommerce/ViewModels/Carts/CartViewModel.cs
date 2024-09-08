using ClickMart.Models.Models;

namespace ClickMart.ViewModels.Carts
{
    public class CartViewModel
    {
        public List<Cart> Carts { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal Discount { get; set; }

        public List<Product> RecommendedProducts { get; set; }

        public bool coupon {  get; set; }
        public decimal CouponDisocunt { get; set; }

        public List<ShippingMethod> ShippingMethods { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
