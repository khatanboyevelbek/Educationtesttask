using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Application.ViewModels.Teachers;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Domain.Enums;

namespace Educationtesttask.Application.Interfaces
{
    public interface ITeacherService 
	{
		public Task<Teacher> AddAsync(TeacherCreateViewModel viewModel);
		public IQueryable<Teacher> RetrieveAll(Expression<Func<Teacher, bool>> filter);
		public Task<Teacher> RetrieveByIdAsync(Guid id);
		public Task<Teacher> ModifyAsync(TeacherUpdateViewModel viewModel);
		public Task<bool> DeleteAsync(Guid id);
		IQueryable<Teacher> RetrieveAllFilteredByMobileOperators(MobileOperators mobileOperators);
		Task<Subject> RetrieveSubjectOfTeacherThatHasSomeStudnetAndMinValue(Guid id, int hasNumberOfStudents, int minimalGrade);
	}
}
