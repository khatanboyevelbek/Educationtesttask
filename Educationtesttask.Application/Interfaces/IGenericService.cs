using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Educationtesttask.Application.Interfaces
{
	public interface IGenericService<T, U> 
		where T : class
		where U : class
	{
		public Task<T> AddAsync(U viewModel);
		public IQueryable<T> RetrieveAll(Expression<Func<T, bool>> filter = null);
		public Task<T> RetrieveByIdAsync(Guid id);
		public Task<T> ModifyAsync(U viewModel);
		public Task<bool> DeleteAsync(Guid id);
	}
}
