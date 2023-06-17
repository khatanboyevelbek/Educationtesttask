using Educationtesttask.Domain.Entities;
using Educationtesttask.Infrastructure.Data;
using Educationtesttask.Infrastructure.Interfaces;

namespace Educationtesttask.Infrastructure.Repositories
{
	public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
	{
		public TeacherRepository(AppDbContext appDbContext)
			: base(appDbContext)
		{ }
	}
}
