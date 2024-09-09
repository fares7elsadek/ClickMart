using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

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

		public List<Cart> Carts { get; set; } = new List<Cart>();

		public string? shippingMethodId { get; set; }

		public ShippingMethod? ShippingMethod { get; set; }

		public List<OrderHeader> OrderHeaders { get; set; } = new List<OrderHeader>();
	}
}
