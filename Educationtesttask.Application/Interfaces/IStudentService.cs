using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Application.ViewModels.Students;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Domain.Enums;

namespace Educationtesttask.Application.Interfaces
{
    public interface IStudentService
	{
		public Task<Student> AddAsync(StudentCreateViewModel viewModel);
		public IQueryable<Student> RetrieveAll(Expression<Func<Student, bool>> filter);
		public Task<Student> RetrieveByIdAsync(Guid id);
		public Task<Student> ModifyAsync(StudentUpdateViewModel viewModel);
		public Task<bool> DeleteAsync(Guid id);
		IQueryable<Student> RetrieveAllFileteredByBirthDate(int startMonth, int startDay, int endMonth, int endDay);
		IQueryable<Student> RetrieveAllFilteredByMobileOperators(MobileOperators mobileOperators);
		IQueryable<Student> RetrieveAllThatContainEnteredPhrase(string phrase);
		Task<Subject> RetrieveStudentSubjectsOfStudentWithHighGrade(Guid id, bool highGrade);
	}
}
