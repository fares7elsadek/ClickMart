using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.Models.Models
{
    public class Product
    {
        public string? Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Range(0,int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue)]
        public decimal DiscountPrice { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        public string ShortDescription { get; set; }
        public bool Published { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CategoryId { get; set; }
        public Category? Category { get; set; }

        public string? Thumbnail {  get; set; }
        public List<Galleries> Galleries { get; set; } = new List<Galleries>();
        public List<Attributes> Attributes { get; set; } = new List<Attributes>();
        public List<ProductAttributes> ProductAttributes { get; set; } = new List<ProductAttributes>();

        public List<Reviews> Reviews { get; set; } = new List<Reviews>();

        public List<Cart> carts { get; set; } = new List<Cart>();

        public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

        public List<Coupons> Coupons { get; set; } = new List<Coupons>();
        public List<ProductCoupons> ProductCoupons { get; set; } = new List<ProductCoupons>();
    }
}
