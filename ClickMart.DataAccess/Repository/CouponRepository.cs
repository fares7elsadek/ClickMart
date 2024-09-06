using ClickMart.DataAccess.Data;
using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using System;

namespace ClickMart.DataAccess.Repository
{
    public class CouponRepository : Repository<Coupons>, ICouponRepository
    {
        public AppDbContext _db;
        public CouponRepository(AppDbContext db): base(db) 
        {
            this._db = db;
        }

        public void Update(Coupons coupon)
        {
           this._db.Coupons.Update(coupon);
        }
    }
}
