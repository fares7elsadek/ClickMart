using ClickMart.DataAccess.Data;
using ClickMart.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
        private IDbContextTransaction _transaction;

        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public IAttributesRepository Attributes { get; private set; }

        public ICountryRepository Countries { get; private set; }

        public IUserRepository Users { get; private set; }

        public IReviewRepository Reviews { get; private set; }

        public ICompanyRepository Company { get; private set; }

        public ICartRepository Cart { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }

        public IOrderDetailRepository OrderDetail { get; private set; }

        public ICouponRepository Coupon { get; private set; }

        public IShippingMethodRepository ShippingMethod { get; private set; }

        public IAddressRepository Address { get; private set; }

        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db)
        {
            this._db = db;
            Category = new CategoryRepository(this._db);
            Product = new ProductRepository(this._db);
            Attributes = new AttributesRepository(this._db);
            Countries = new CountryRepository(this._db);
            Users = new UserRepository(this._db);
            Reviews = new ReviewRepository(this._db);
            Company = new CompanyRepository(this._db);
            Cart = new CartRepository(this._db);
            OrderHeader = new OrderHeaderRepository(this._db);
            OrderDetail = new OrderDetailRepository(this._db);
            Coupon = new CouponRepository(this._db);
            ShippingMethod = new ShippingMethodRepository(this._db);
            Address = new AddressRepository(this._db);

        }
        public void Save()
        {
            this._db.SaveChanges();
        }

        public IDbContextTransaction BeginTransaction()
        {
            _transaction = _db.Database.BeginTransaction();
            return _transaction;
        }

        public void Commit()
        {
            try
            {
                Save();  
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
            }
        }
        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

    }
}
