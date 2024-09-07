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
           Coupons newCoupon = this._db.Coupons.FirstOrDefault(c => c.Id==coupon.Id);
            if (newCoupon != null)
            {
                newCoupon.code = coupon.code;
                newCoupon.couponDescription = coupon.couponDescription;
                newCoupon.couponStartDate = coupon.couponStartDate;
                newCoupon.couponEndDate = coupon.couponEndDate;
                newCoupon.maxUsage = coupon.maxUsage;
                newCoupon.discountValue = coupon.discountValue;
                this._db.Coupons.Update(newCoupon);
            }          
        }
    }
}
