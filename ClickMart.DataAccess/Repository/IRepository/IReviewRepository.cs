using ClickMart.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.DataAccess.Repository.IRepository
{
    public interface IReviewRepository: IRepository<Reviews>
    {
        IEnumerable<Reviews> GetProductReview(Expression<Func<Reviews,bool>> filter
            , string? IncludeProperties = null);
        void Update(Reviews review);
    }
}
