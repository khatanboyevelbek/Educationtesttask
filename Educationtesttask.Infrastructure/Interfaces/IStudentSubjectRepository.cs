using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Domain.Entities;

namespace Educationtesttask.Infrastructure.Interfaces
{
	public interface IStudentSubjectRepository
	{
		public Task<StudentSubject> AddAsync(StudentSubject entity);
		public Task<StudentSubject> SelectByIdAsync(Guid id1, Guid id2);
		public IQueryable<StudentSubject> SelectAllAsync(Expression<Func<StudentSubject, bool>> expression = null);
		public Task<StudentSubject> UpdateAsync(StudentSubject entity);
		public Task<bool> DeleteAsync(StudentSubject entity);
	}
}
