using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Domain.Exceptions.Students;
using Educationtesttask.Domain.Exceptions.StudentSubjects;
using FluentValidation.Results;

namespace Educationtesttask.Application.Services.StudentSubjects
{
	public partial class StudentSubjectService
	{
		private static void Validate(ValidationResult validationResult)
		{
			var invalidStudentSubjectException = new InvalidStudentSubjectException();

			foreach (var error in validationResult.Errors)
			{
				invalidStudentSubjectException.UpsertDataList(error.PropertyName, error.ErrorMessage);
			}

			invalidStudentSubjectException.ThrowIfContainsErrors();
		}
	}
}
