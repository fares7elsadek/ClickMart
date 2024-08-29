using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ClickMart.DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> GetAll(string? IncludeProperties = null);

		IEnumerable<T> GetAllWithCondition(Expression<Func<T, bool>> filter, string? IncludeProperties = null);
        T GetOrDefalut(Expression<Func<T,bool>> filter, string? IncludeProperties = null);
		void Add(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);
	}
}
