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

namespace Educationtesttask.Infrastructure.Repositories
{
	public class StudentRepository : GenericRepository<Student>, IStudentRepository
	{
		private readonly AppDbContext appDbContext;

		public StudentRepository(AppDbContext appDbContext)
			: base(appDbContext)
		{ 
			this.appDbContext = appDbContext;
		}

		public async Task<Student> SelectStudentByEmail(string email)
		{
			return this.appDbContext.Set<Student>().FirstOrDefault(s => s.Email == email);
		}

		public override IQueryable<Student> SelectAllAsync(Expression<Func<Student, bool>> filter = null)
		{
			return filter is null ? this.appDbContext.Set<Student>().Include(s => s.StudentSubjects) :
				this.appDbContext.Set<Student>().Where(filter).Include(s => s.StudentSubjects);
		}
	}
}
