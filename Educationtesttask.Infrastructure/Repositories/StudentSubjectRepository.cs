using System.Linq.Expressions;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Infrastructure.Data;
using Educationtesttask.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace Educationtesttask.Infrastructure.Repositories
{
	public class StudentSubjectRepository : IStudentSubjectRepository
	{
		private readonly AppDbContext appDbContext;
		private readonly DbSet<StudentSubject> dbSet;

		public StudentSubjectRepository (AppDbContext appDbContext)
		{
			this.appDbContext = appDbContext;
			this.dbSet = appDbContext.Set<StudentSubject>();
		}

		public async Task<StudentSubject> AddAsync(StudentSubject entity)
		{
			EntityEntry<StudentSubject> addedEntity = await dbSet.AddAsync(entity);
			await appDbContext.SaveChangesAsync();

			return addedEntity.Entity;
		}

		public async Task<StudentSubject> SelectByIdAsync(Guid id1, Guid id2) =>
			await this.dbSet.FindAsync(id1, id2);

		public IQueryable<StudentSubject> SelectAllAsync(Expression<Func<StudentSubject, bool>> filter = null)
		{
			return filter is null ? this.dbSet.Include(s => s.Subject) : this.dbSet.Where(filter).Include(s => s.Subject);
		}

		public async Task<StudentSubject> UpdateAsync(StudentSubject entity)
		{
			EntityEntry<StudentSubject> updatedEntity = this.dbSet.Update(entity);
			await this.appDbContext.SaveChangesAsync();

			return updatedEntity.Entity;
		}

		public async Task<bool> DeleteAsync(StudentSubject entity)
		{
			EntityEntry<StudentSubject> updatedEntity = this.dbSet.Remove(entity);
			int result = await this.appDbContext.SaveChangesAsync();

			return result > 0;
		}
	}
}
