using ClickMart.DataAccess.Data;
using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using System;

namespace ClickMart.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        public AppDbContext _db;
        public OrderHeaderRepository(AppDbContext db): base(db) 
        {
            this._db = db;
        }

        public void Update(OrderHeader orderHeader)
        {
           this._db.OrderHeaders.Update(orderHeader);
        }
    }
}
