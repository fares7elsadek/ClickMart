using Microsoft.AspNetCore.Identity;

namespace ClickMart.Models.Models
{
	public class User : IdentityUser
	{
		public List<UserAddress> Address { get; set; } = new List<UserAddress>();
	}
}
