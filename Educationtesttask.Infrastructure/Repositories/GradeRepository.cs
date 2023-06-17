using Educationtesttask.Domain.Entities;
using Educationtesttask.Infrastructure.Data;
using Educationtesttask.Infrastructure.Interfaces;

namespace Educationtesttask.Infrastructure.Repositories
{
	public class GradeRepository : GenericRepository<Grade>, IGradeRepository
	{
		public GradeRepository(AppDbContext appDbContext) 
			: base(appDbContext)
		{ }
	}
}
