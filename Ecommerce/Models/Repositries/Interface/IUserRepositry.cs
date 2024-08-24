using Ecommerce.ViewModels.Account;

namespace Ecommerce.Models.Repositries.Interface
{
	public interface IUserRepositry
	{
		public void RegisterUser(RegisterViewModel User);
		public bool LoginUser(LoginViewModel User);
	}
}
