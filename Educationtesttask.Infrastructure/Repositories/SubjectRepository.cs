using Educationtesttask.Domain.Entities;
using Educationtesttask.Infrastructure.Data;
using Educationtesttask.Infrastructure.Interfaces;

namespace Educationtesttask.Infrastructure.Repositories
{
	public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
	{
		public SubjectRepository(AppDbContext appDbContext) 
			: base(appDbContext)
		{ }
	}
}
