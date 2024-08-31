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
    public class AddressRepository:Repository<Address>,IAddressRepository
    {
        private AppDbContext _db;
        public AddressRepository(AppDbContext db) : base(db)
        {
            this._db = db;
        }
        public void Update(Address address)
        {
            this._db.Address.Update(address);
        }
    }
}
