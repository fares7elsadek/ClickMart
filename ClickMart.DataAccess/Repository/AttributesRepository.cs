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
    public class AttributesRepository : Repository<Attributes>, IAttributesRepository
    {
        private AppDbContext _db;
        public AttributesRepository(AppDbContext db) : base(db)
        {
            this._db = db;
        }
        public void Update(Attributes attribute)
        {
           this._db.Attributes.Update(attribute);
        }
    }
}
