using System.Linq.Expressions;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Infrastructure.Data;
using Educationtesttask.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Educationtesttask.Infrastructure.Repositories
{
	public class TeacherRepository : ITeacherRepository
	{
		private readonly AppDbContext appDbContext;
		private readonly DbSet<Teacher> dbSet;

		public TeacherRepository(AppDbContext appDbContext)
		{
			this.appDbContext = appDbContext;
			this.dbSet = appDbContext.Set<Teacher>();
		}

		public async Task<Teacher> AddAsync(Teacher entity)
		{
			EntityEntry<Teacher> addedEntity = await dbSet.AddAsync(entity);
			await appDbContext.SaveChangesAsync();

			return addedEntity.Entity;
		}

		public async Task<Teacher> SelectByIdAsync(Guid id1) =>
			this.dbSet.Include(t => t.Subjects).ThenInclude(ss => ss.StudentSubjects).FirstOrDefault(t => t.Id == id1);

		public async Task<Teacher> UpdateAsync(Teacher entity)
		{
			EntityEntry<Teacher> updatedEntity = this.dbSet.Update(entity);
			await this.appDbContext.SaveChangesAsync();

			return updatedEntity.Entity;
		}

		public async Task<bool> DeleteAsync(Teacher entity)
		{
			EntityEntry<Teacher> updatedEntity = this.dbSet.Remove(entity);
			int result = await this.appDbContext.SaveChangesAsync();

			return result > 0;
		}

		public async Task<Teacher> SelectTeacherByEmail(string email)
		{
			return this.dbSet.FirstOrDefault(t => t.Email == email);
		}

		public IQueryable<Teacher> SelectAllAsync(Expression<Func<Teacher, bool>> filter = null)
		{
			return filter is null ? this.dbSet.Include(t => t.Subjects).ThenInclude(ss => ss.StudentSubjects) :
				this.dbSet.Where(filter).Include(t => t.Subjects).ThenInclude(ss => ss.StudentSubjects);
		}
	}
}
