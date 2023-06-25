using Educationtesttask.Domain.Entities;

namespace Educationtesttask.Infrastructure.Interfaces
{
	public interface ITeacherRepository : IGenericRepository<Teacher>
	{
		public Task<Teacher> SelectTeacherByEmail(string email);
	}
}
