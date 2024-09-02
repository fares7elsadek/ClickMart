using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.Models.Models
{
    public class Company
    {
        public string? Id {  get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [DisplayName("Street address"),MaxLength(255)]
        public string StreetAddress { get; set; }

        [MaxLength(255)]
        public string City { get; set; }

        [MaxLength(255)]
        public string State { get; set; }

        [DisplayName("Postal code"), MaxLength(5)]
        public string PostalCode { get; set; }

        [DisplayName("Phone number"), MaxLength(12)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}
