using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.Models.Models
{
    public class ShippingMethod
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public bool Default { get; set; }
        public List<OrderHeader> OrderHeaders { get; set; }
    }
}
