using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.Models.Models
{
    public class Attributes
    {
        public string Id { get; set; }

        public string AttributeName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string AttributeValue { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
        public List<ProductAttributes> ProductAttributes { get; set; } = new List<ProductAttributes>();
    }
}
