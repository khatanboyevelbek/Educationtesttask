using System.Linq.Expressions;
using Educationtesttask.Domain.DTOs.StudentSubjects;
using Educationtesttask.Domain.Entities;

namespace Educationtesttask.Application.Interfaces
{
    public interface IStudentSubjectService
	{
		public Task<StudentSubject> AddAsync(StudentSubjectDto viewModel);
		public IQueryable<StudentSubject> RetrieveAll(Expression<Func<StudentSubject, bool>> filter);
		public Task<StudentSubject> RetrieveByIdAsync(Guid id1, Guid id2);
		public Task<StudentSubject> ModifyAsync(StudentSubjectDto viewModel);
		public Task<bool> DeleteAsync(Guid id1, Guid id2);
	}
}
