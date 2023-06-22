using System.Linq.Expressions;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Infrastructure.Data;
using Educationtesttask.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace Educationtesttask.Infrastructure.Repositories
{
	public class StudentSubjectRepository : IStudentSubjectRepository<StudentSubject>
	{
		private readonly AppDbContext dbContext;
		private readonly DbSet<StudentSubject> dbSet;

		public StudentSubjectRepository (AppDbContext dbContext)
		{
			this.dbContext = dbContext;
			this.dbSet = dbContext.Set<StudentSubject>();
		}

		public async Task<StudentSubject> AddAsync(StudentSubject entity)
		{
			EntityEntry<StudentSubject> addedEntity = await dbSet.AddAsync(entity);
			await dbContext.SaveChangesAsync();

			return addedEntity.Entity;
		}

		public async Task<StudentSubject> SelectByIdAsync(Guid id1, Guid id2) =>
			await this.dbSet.FindAsync(id1, id2);

		public IQueryable<StudentSubject> SelectAllAsync(Expression<Func<StudentSubject, bool>> filter = null) =>
			this.dbSet.Where(filter);

		public async Task<StudentSubject> UpdateAsync(StudentSubject entity)
		{
			EntityEntry<StudentSubject> updatedEntity = this.dbSet.Update(entity);
			await this.dbContext.SaveChangesAsync();

			return updatedEntity.Entity;
		}

		public async Task<bool> DeleteAsync(StudentSubject entity)
		{
			EntityEntry<StudentSubject> updatedEntity = this.dbSet.Remove(entity);
			int result = await this.dbContext.SaveChangesAsync();

			return result > 0;
		}
	}
}
