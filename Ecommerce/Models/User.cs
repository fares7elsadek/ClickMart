using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Models
{
    public class User: IdentityUser
    {
        public List<UserAddress> Address { get; set; } = new List<UserAddress>();
    }
}
