using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.Models.Models
{
    public class OrderDetails
    {
        public string Id { get; set; }
        [Required]
        public string OrderHeaderId { get; set; }

        public OrderHeader OrderHeader { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }


        public int qty { get; set; }

        public double Price { get; set; }
    }
}
