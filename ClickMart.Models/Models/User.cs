using Microsoft.AspNetCore.Identity;

namespace ClickMart.Models.Models
{
	public class User : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public string? avatar {  get; set; }
		public List<UserAddress> Address { get; set; } = new List<UserAddress>();
		public List<Address> AddressList { get; set; } = new List<Address>();
	}
}
