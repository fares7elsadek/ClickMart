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

        public string? ImageUrl { get; set; }
        public Category? Category { get; set; }
        public List<Galleries> Galleries { get; set; } = new List<Galleries>();
        public List<Attributes> Attributes { get; set; } = new List<Attributes>();
        public List<ProductAttributes> ProductAttributes { get; set; } = new List<ProductAttributes>();
    }
}
