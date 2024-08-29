using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.Models.Models
{
    public class ProductAttributes
    {
        public string ProductId { get; set; }
        public string AttributeId { get; set; }

        public Product Product { get; set; }

        public Attributes attribute { get; set; }
    }
}
