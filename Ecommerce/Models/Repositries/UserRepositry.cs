using Ecommerce.Models.config;
using Ecommerce.Models.Repositries.Interface;
using Ecommerce.ViewModels.Account;

namespace Ecommerce.Models.Repositries
{
	public class UserRepositry : IUserRepositry
	{
		private readonly AppDbContext _context;
        public UserRepositry(AppDbContext context)
        {
            this._context = context;
        }
        public bool LoginUser(LoginViewModel User)
		{
			throw new NotImplementedException();
		}

		public void RegisterUser(RegisterViewModel User)
		{
			throw new NotImplementedException();
		}
	}
}
