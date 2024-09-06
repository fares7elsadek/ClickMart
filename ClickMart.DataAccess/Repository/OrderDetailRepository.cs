using ClickMart.DataAccess.Data;
using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using System;

namespace ClickMart.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetails>, IOrderDetailRepository
    {
        public AppDbContext _db;
        public OrderDetailRepository(AppDbContext db): base(db) 
        {
            this._db = db;
        }

        public void Update(OrderDetails orderDetails)
        {
           this._db.OrderDetails.Update(orderDetails);
        }
    }
}
