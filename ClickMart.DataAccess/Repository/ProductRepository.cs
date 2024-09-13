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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private AppDbContext _db;
        public ProductRepository(AppDbContext db): base(db) 
        {
            this._db = db;
        }
        public void Update(Product product)
        {
            var newProduct = this._db.Products.FirstOrDefault(p => p.Id == product.Id);
            if (newProduct != null)
            {
                newProduct.Price = product.Price;
                newProduct.Description = product.Description;
                newProduct.Title = product.Title;
                newProduct.CategoryId = product.CategoryId;
                newProduct.ShortDescription = product.ShortDescription;
                newProduct.DiscountPrice = product.DiscountPrice;
                newProduct.Published = product.Published;
                newProduct.Quantity = product.Quantity;
                if(product.Galleries.Count > 0)
                {
                    foreach(var galary in product.Galleries)
                    {
                        newProduct.Galleries.Add(galary);
                    }
                }
            }
        }
    }
}
