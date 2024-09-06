using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClickMart.Models.Models
{
	public class Address
	{
		public string Id { get; set; }
		[Required]
		[MaxLength(5)]
        [DisplayName("Unit Number")]
        public string UnitNumber { get; set; }

		[Required]
		[MaxLength(5)]
        [DisplayName("Street number")]
        public string StreetNumber { get; set; }

		[Required]
        [DisplayName("Address line 1")]
        public string AddressLine1 { get; set; }

        [DisplayName("Address line 2")]
        public string? AddressLine2 { get; set; }

		public string City { get; set; }

		public string Region { get; set; }

		[DataType(DataType.PostalCode)]
		[DisplayName("Postal code")]
		public string PostalCode { get; set; }

		public string CountryId { get; set; }

		public bool IsDefault { get; set; }
		public Country Country { get; set; }
		public string UserId { get; set; }
		public User User { get; set; }

		public List<OrderHeader> Order { get; set; } = new List<OrderHeader>();
	}
}
