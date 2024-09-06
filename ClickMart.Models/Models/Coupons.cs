using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.Models.Models
{
    public class Coupons
    {
        public string Id { get; set; }

        public string code { get; set; }
        [DisplayName("Coupon Name")]
        public string couponDescription { get; set; }
        [DisplayName("Discount value")]
        public decimal discountValue { get; set; }

        public int? timesUsed { get; set; }

        public int maxUsage { get; set; }

        public DateTime couponStartDate { get; set; }

        public DateTime couponEndDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<OrderHeader> OrderHeaders { get; set; } = new List<OrderHeader>();

        public List<ProductCoupons> ProductCoupons { get; set; } = new List<ProductCoupons>();
        public List<Product> products { get; set; } = new List<Product>();
    }
}
