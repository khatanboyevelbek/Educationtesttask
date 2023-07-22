using System.Linq.Expressions;
using Educationtesttask.Domain.DTOs.Teachers;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Domain.Enums;

namespace Educationtesttask.Application.Interfaces
{
    public interface ITeacherService 
	{
		public Task<Teacher> AddAsync(TeacherCreateDto viewModel);
		public IQueryable<Teacher> RetrieveAll(Expression<Func<Teacher, bool>> filter);
		public Task<Teacher> RetrieveByIdAsync(Guid id);
		public Task<Teacher> ModifyAsync(TeacherUpdateDto viewModel);
		public Task<bool> DeleteAsync(Guid id);
		IQueryable<Teacher> RetrieveAllFilteredByMobileOperators(MobileOperators mobileOperators);
		Task<Subject> RetrieveSubjectOfTeacherThatHasSomeStudnetAndMinValue(Guid id, int hasNumberOfStudents, int minimalGrade);
		IQueryable<Teacher> RetrieveAllTeachersThatTeachSubjectsWithHighestGradeThanEnteredValue(int minGrade);
	}
}
