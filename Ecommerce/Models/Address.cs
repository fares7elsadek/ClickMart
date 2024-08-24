using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Address
    {
        public string Id { get; set; }
        [Required]
        [MaxLength(5)]
        public string UnitNumber { get; set; }

        [Required]
        [MaxLength(5)]
        public string StreetNumber { get; set; }

        [Required]
        public string AddressLine1 { get; set; }
        
        public string? AddressLine2 { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string CountryId { get; set; }

        public Country Country { get; set; }
        public List<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

    }
}
