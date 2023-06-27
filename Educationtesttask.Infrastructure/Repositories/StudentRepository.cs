using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Infrastructure.Data;
using Educationtesttask.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Educationtesttask.Infrastructure.Repositories
{
	public class StudentRepository : IStudentRepository
	{
		private readonly AppDbContext appDbContext;
		private readonly DbSet<Student> dbSet;

		public StudentRepository(AppDbContext appDbContext)
		{ 
			this.appDbContext = appDbContext;
			this.dbSet = appDbContext.Set<Student>();
		}

		public async Task<Student> AddAsync(Student entity)
		{
			EntityEntry<Student> addedEntity = await dbSet.AddAsync(entity);
			await appDbContext.SaveChangesAsync();

			return addedEntity.Entity;
		}

		public async Task<Student> SelectByIdAsync(Guid id) =>
			this.dbSet.Include(s => s.StudentSubjects).ThenInclude(s => s.Subject).FirstOrDefault(s => s.Id == id);


		public async Task<Student> UpdateAsync(Student entity)
		{
			EntityEntry<Student> updatedEntity = this.dbSet.Update(entity);
			await this.appDbContext.SaveChangesAsync();

			return updatedEntity.Entity;
		}

		public async Task<bool> DeleteAsync(Student entity)
		{
			EntityEntry<Student> updatedEntity = this.dbSet.Remove(entity);
			int result = await this.appDbContext.SaveChangesAsync();

			return result > 0;
		}

		public async Task<Student> SelectStudentByEmail(string email)
		{
			return this.dbSet.FirstOrDefault(s => s.Email == email);
		}

		public IQueryable<Student> SelectAllAsync(Expression<Func<Student, bool>> filter = null)
		{
			return filter is null ? this.dbSet.Include(s => s.StudentSubjects).ThenInclude(s => s.Subject) :
				this.dbSet.Where(filter).Include(s => s.StudentSubjects).ThenInclude(s => s.Subject);
		}
	}
}
