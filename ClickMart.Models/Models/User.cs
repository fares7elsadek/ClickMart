using Microsoft.AspNetCore.Identity;

namespace ClickMart.Models.Models
{
	public class User : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public string? avatar {  get; set; }
		public List<Address> Addresses { get; set; } = new List<Address>();

		public List<Reviews> Reviews { get; set; } = new List<Reviews>();

		public string? CompanyId { get; set; }

		public Company Company { get; set; }
	}
}
