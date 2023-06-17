using System.Linq.Expressions;

namespace Educationtesttask.Infrastructure.Interfaces
{
	public interface IGenericRepository<T>
	{
		public Task<T> AddAsync(T entity);
		public Task<T> SelectByIdAsync(Guid id);
		public IQueryable<T> SelectAllAsync(Expression<Func<T, bool>> expression = null);
		public Task<T> UpdateAsync(T entity);
		public Task<bool> DeleteAsync(T entity);
	}
}
