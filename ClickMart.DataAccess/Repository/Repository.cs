using ClickMart.DataAccess.Data;
using ClickMart.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		public AppDbContext _Db { get; }
		public DbSet<T> DbSet { get; }
		public Repository(AppDbContext Db)
        {
			this._Db = Db;
			DbSet = Db.Set<T>();
		}
		public void Add(T entity)
		{
			this.DbSet.Add(entity);
		}

		public IEnumerable<T> GetAll(string? IncludeProperties = null)
		{
			IQueryable<T> query = this.DbSet;
			if (!string.IsNullOrEmpty(IncludeProperties))
			{
				foreach(var property in IncludeProperties
					.Split(new char[] { ','} ,StringSplitOptions.RemoveEmptyEntries))
				{
                    query = query.Include(property);
                }
			}
			return query.ToList();
		}

		public T GetOrDefalut(Expression<Func<T, bool>> filter,string? IncludeProperties=null)
		{
			IQueryable<T> query = DbSet;
            if (!string.IsNullOrEmpty(IncludeProperties))
            {
                foreach (var property in IncludeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            query = query.Where(filter);
			return query.FirstOrDefault();
		}

		public void Remove(T entity)
		{
			DbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			DbSet.RemoveRange(entities);
		}
	}
}
