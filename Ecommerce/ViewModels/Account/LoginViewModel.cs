using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClickMart.ViewModels.Account
{
	public class LoginViewModel
	{
		[Required]
		[DataType(DataType.EmailAddress, ErrorMessage = "Wrong or Invalid email address")]
		[DisplayName("Email address")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[DisplayName(@"Password")]
		public string Password { get; set; }
	}
}
