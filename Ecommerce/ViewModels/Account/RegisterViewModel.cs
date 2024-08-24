using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ViewModels.Account
{
    public class RegisterViewModel
    {
		[Required]
		[MaxLength(255)]
        [DisplayName("Username")]
		public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
        [DisplayName("Re-enter password")]
		public string ConfirmPassword { get; set; }
    }
}
