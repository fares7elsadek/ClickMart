using ClickMart.DataAccess.Data;
using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.DataAccess.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly AppDbContext _db;
        public CartRepository(AppDbContext db):base(db) 
        {
            this._db = db;
        }
        public void Update(Cart cart)
        {
            this._db.Carts.Update(cart);
        }
    }
}
