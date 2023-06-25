using System.Linq.Expressions;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Infrastructure.Data;
using Educationtesttask.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Educationtesttask.Infrastructure.Repositories
{
	public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
	{
		private readonly AppDbContext appDbContext;

		public TeacherRepository(AppDbContext appDbContext)
			: base(appDbContext)
		{
			this.appDbContext = appDbContext;
		}

		public async Task<Teacher> SelectTeacherByEmail(string email)
		{
			return this.appDbContext.Set<Teacher>().FirstOrDefault(t => t.Email == email);
		}

		public override IQueryable<Teacher> SelectAllAsync(Expression<Func<Teacher, bool>> filter = null)
		{
			return filter is null ? this.appDbContext.Set<Teacher>().Include(t => t.Subjects) : 
				this.appDbContext.Set<Teacher>().Where(filter).Include(t => t.Subjects);
		}
	}
}
