using Educationtesttask.Domain.Entities;
using Educationtesttask.Infrastructure.Data;
using Educationtesttask.Infrastructure.Interfaces;

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
	}
}
