using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }

        IAttributesRepository Attributes { get; }
        ICountryRepository Countries { get; }

        IUserRepository Users { get; }

        IReviewRepository Reviews { get; }
        void Save();
    }
}
