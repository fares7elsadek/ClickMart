using ClickMart.DataAccess.Data;
using ClickMart.DataAccess.Repository.IRepository;
using ClickMart.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.DataAccess.Repository
{
    public class ReviewRepository : Repository<Reviews>, IReviewRepository
    {
        private readonly AppDbContext _db;
        public ReviewRepository(AppDbContext db):base(db)
        {
            _db = db;
        }
        public IEnumerable<Reviews> GetProductReview(Expression<Func<Reviews, bool>> filter,
            string? IncludeProperties = null)
        {
            IQueryable<Reviews> query = this._db.Reviews;
            if (!string.IsNullOrEmpty(IncludeProperties))
            {
                foreach (var property in IncludeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.Where(filter);
        }

        public void Update(Reviews review)
        {
            this._db.Reviews.Update(review);
        }
    }
}
