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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private  AppDbContext _db;
        public CompanyRepository(AppDbContext db):base(db) 
        {
            this._db = db;
        }
        public void Update(Company company)
        {
            this._db.Companies.Update(company);
        }
    }
}
