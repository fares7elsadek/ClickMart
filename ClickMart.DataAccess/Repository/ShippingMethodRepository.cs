using ClickMart.DataAccess.Data;
using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using System;

namespace ClickMart.DataAccess.Repository
{
    public class ShippingMethodRepository : Repository<ShippingMethod>, IShippingMethodRepository
    {
        public AppDbContext _db;
        public ShippingMethodRepository(AppDbContext db): base(db) 
        {
            this._db = db;
        }

        public void Update(ShippingMethod shippingMethod)
        {
           this._db.ShippingMethods.Update(shippingMethod);
        }
    }
}
