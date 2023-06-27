using System.Linq.Expressions;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Infrastructure.Data;
using Educationtesttask.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Educationtesttask.Infrastructure.Repositories
{
	public class SubjectRepository : ISubjectRepository
	{
		private readonly AppDbContext appDbContext;
		private readonly DbSet<Subject> dbSet;

		public SubjectRepository(AppDbContext appDbContext)
		{
			this.appDbContext = appDbContext;
			this.dbSet = appDbContext.Set<Subject>();
		}

		public async Task<Subject> AddAsync(Subject entity)
		{
			EntityEntry<Subject> addedEntity = await dbSet.AddAsync(entity);
			await appDbContext.SaveChangesAsync();

			return addedEntity.Entity;
		}

		public async Task<Subject> SelectByIdAsync(Guid id) =>
			await this.dbSet.FindAsync(id);


		public async Task<Subject> UpdateAsync(Subject entity)
		{
			EntityEntry<Subject> updatedEntity = this.dbSet.Update(entity);
			await this.appDbContext.SaveChangesAsync();

			return updatedEntity.Entity;
		}

		public async Task<bool> DeleteAsync(Subject entity)
		{
			EntityEntry<Subject> updatedEntity = this.dbSet.Remove(entity);
			int result = await this.appDbContext.SaveChangesAsync();

			return result > 0;
		}

		public IQueryable<Subject> SelectAllAsync(Expression<Func<Subject, bool>> filter = null)
		{
			return filter is null ? this.dbSet.Include(s => s.StudentSubjects) :
				this.dbSet.Where(filter).Include(s => s.StudentSubjects);
		}
	}
}
