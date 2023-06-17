using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Infrastructure.Data;
using Educationtesttask.Infrastructure.Interfaces;

namespace Educationtesttask.Infrastructure.Repositories
{
	public class StudentRepository : GenericRepository<Student>, IStudentRepository
	{
		public StudentRepository(AppDbContext appDbContext)
			: base(appDbContext)
		{ }
	}
}
