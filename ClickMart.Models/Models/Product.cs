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

        public string CategoryId { get; set; }

        public Category Category { get; set; }

        public string ImageUrl { get; set; }
    }
}
