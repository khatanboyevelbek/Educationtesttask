using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Application.ViewModels.Students;
using Educationtesttask.Application.ViewModels.StudentSubjects;
using Educationtesttask.Domain.Entities;

namespace Educationtesttask.Application.Interfaces
{
	public interface IStudentSubjectService
	{
		public Task<StudentSubject> AddAsync(StudentSubjectViewModel viewModel);
		public IQueryable<StudentSubject> RetrieveAll(Expression<Func<StudentSubject, bool>> filter);
		public Task<StudentSubject> RetrieveByIdAsync(Guid id1, Guid id2);
		public Task<StudentSubject> ModifyAsync(StudentSubjectViewModel viewModel);
		public Task<bool> DeleteAsync(Guid id1, Guid id2);
	}
}
