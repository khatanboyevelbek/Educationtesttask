using System.Linq.Expressions;

namespace Educationtesttask.Infrastructure.Interfaces
{
	public interface IGenericRepository<T>
	{
		Task<T> AddAsync(T entity);
		Task<T> SelectByIdAsync(Guid id);
		IQueryable<T> SelectAllAsync(Expression<Func<T, bool>> expression = null);
		Task<T> UpdateAsync(T entity);
		Task<bool> DeleteAsync(T entity);
	}
}
