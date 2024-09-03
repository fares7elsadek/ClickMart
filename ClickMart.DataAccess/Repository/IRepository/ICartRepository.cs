using ClickMart.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.DataAccess.Repository.IRepository
{
    public interface ICartRepository: IRepository<Cart>
    {
        void Update(Cart cart);
    }
}
