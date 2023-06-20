using Educationtesttask.Domain.Entities;
using Educationtesttask.Infrastructure.Data;
using Educationtesttask.Infrastructure.Interfaces;

namespace Educationtesttask.Infrastructure.Repositories
{
	public class StudentSubjectRepository : GenericRepository<StudentSubject>, IStudentSubjectRepository
	{
		public StudentSubjectRepository (AppDbContext appDbContext)
			: base(appDbContext)
		{ }
	}
}
