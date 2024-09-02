using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.Models.Models
{
    public class Reviews
    {
        public string? Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }

        [Required]
        [Range(1, 5)]
        public int Stars { get; set; }

        public bool? Verified { get; set; }

        public string? ProductId { get; set; }
        public string? UserId { get; set; }

        public Product? Product { get; set; }
        public User? User { get; set; }
    }
}
