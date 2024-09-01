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
    public class UserRepository: Repository<User> , IUserRepository
    {
        private AppDbContext _db;
        public UserRepository(AppDbContext db) : base(db)
        {
            this._db = db;
        }

        public User GetUserWithAddresses(Expression<Func<User, bool>> filter)
        {

            var user = _db.Users
                    .Include(u => u.Addresses)
                    .ThenInclude(a => a.Country)
                    .FirstOrDefault(filter);
            return user;
        }
    }
}
