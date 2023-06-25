using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Domain.Entities;

namespace Educationtesttask.Infrastructure.Interfaces
{
	public interface IStudentRepository : IGenericRepository<Student>
	{
		public Task<Student> SelectStudentByEmail (string email);
	}
}
