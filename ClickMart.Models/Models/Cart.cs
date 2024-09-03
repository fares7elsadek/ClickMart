using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.Models.Models
{
    public class Cart
    {
        
        public string Id { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int Quantity { get; set; }


    }
}
