using System.Linq.Expressions;
using Educationtesttask.Infrastructure.Data;
using Educationtesttask.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Educationtesttask.Infrastructure.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly AppDbContext dbContext;
		private readonly DbSet<T> dbSet;

		public GenericRepository(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
			this.dbSet = dbContext.Set<T>();
		}

		public async Task<T> AddAsync(T entity)
		{
			EntityEntry<T> addedEntity = await dbSet.AddAsync(entity);
			await dbContext.SaveChangesAsync();

			return addedEntity.Entity;
		}

		public async Task<T> SelectByIdAsync(Guid id) =>
			await this.dbSet.FindAsync(id);

		public IQueryable<T> SelectAllAsync(Expression<Func<T, bool>> filter = null) =>
			this.dbSet.Where(filter);

		public async Task<T> UpdateAsync(T entity)
		{
			EntityEntry<T> updatedEntity = this.dbSet.Update(entity);
			await this.dbContext.SaveChangesAsync();

			return updatedEntity.Entity;
		}

		public async Task<bool> DeleteAsync(T entity)
		{
			EntityEntry<T> updatedEntity = this.dbSet.Remove(entity);
			int result = await this.dbContext.SaveChangesAsync();

			return result > 0;
		}
	}
}
