using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.Models.Models
{
    public class Galleries
    {
        public string Id { get; set; }
        public string productId { get; set; }
        public Product product { get; set; }
        public string ImagePath { get; set; }
        public bool thumbnail { get; set; }
        public int displayOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
