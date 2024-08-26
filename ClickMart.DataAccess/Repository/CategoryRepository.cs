using ClickMart.DataAccess.Data;
using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using System;

namespace ClickMart.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public AppDbContext _db;
        public CategoryRepository(AppDbContext db): base(db) 
        {
            this._db = db;
        }

        public void Update(Category category)
        {
           this._db.Update(category);
        }
    }
}
