using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.Models.Models
{
    public class Category
    {
        public string? Id { get; set; }

        [Required]
        [DisplayName("Category Name")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }

        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}
