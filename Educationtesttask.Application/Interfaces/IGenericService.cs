using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Educationtesttask.Application.Interfaces
{
	public interface IGenericService<TEntity, TCreate, TUpdate> 
		where TEntity : class
		where TUpdate : class
		where TCreate : class
	{
		public Task<TEntity> AddAsync(TCreate viewModel);
		public IQueryable<TEntity> RetrieveAll(Expression<Func<TEntity, bool>> filter);
		public Task<TEntity> RetrieveByIdAsync(Guid id);
		public Task<TEntity> ModifyAsync(TUpdate viewModel);
		public Task<bool> DeleteAsync(Guid id);
	}
}
