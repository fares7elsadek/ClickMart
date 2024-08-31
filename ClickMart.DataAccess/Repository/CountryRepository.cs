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
    public class CountryRepository: Repository<Country>, ICountryRepository
    {
        public AppDbContext _db;
        public CountryRepository(AppDbContext db) : base(db)
        {
            this._db = db;
        }
    }
}
