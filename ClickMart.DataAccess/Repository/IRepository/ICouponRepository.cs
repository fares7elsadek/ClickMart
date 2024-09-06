using ClickMart.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.DataAccess.Repository.IRepository
{
    public interface ICouponRepository: IRepository<Coupons>
    {
        void Update(Coupons coupon);
    }
}
