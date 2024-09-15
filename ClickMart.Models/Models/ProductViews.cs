using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.Models.Models
{
    public class ProductViews
    {
        public string ProductId { get; set; }
        public Product product { get; set; }

        public string UserId { get; set; }

        public User user { get; set; }
    }
}
