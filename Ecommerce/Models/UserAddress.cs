using Microsoft.Identity.Client;

namespace Ecommerce.Models
{
    public class UserAddress
    {
        public string UserId { get; set; }

        public string AddressId { get; set; }

        public bool IsDefault { get; set; }

        public User User { get; set; }

        public Address Address { get; set; }
    }
}
