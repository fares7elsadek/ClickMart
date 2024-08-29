using ClickMart.DataAccess.Data;
using ClickMart.DataAccess.Repository.IRepository;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public IAttributesRepository Attributes { get; private set; }
        private AppDbContext _db;
        public UnitOfWork(AppDbContext db)
        {
            this._db = db;
            Category = new CategoryRepository(this._db);
            Product = new ProductRepository(this._db);
            Attributes = new AttributesRepository(this._db);
        }
        public void Save()
        {
            this._db.SaveChanges();
        }
    }
}
