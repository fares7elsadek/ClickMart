using Microsoft.AspNetCore.Identity;

namespace ClickMart.Models.Models
{
	public class User : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public List<UserAddress> Address { get; set; } = new List<UserAddress>();
	}
}
