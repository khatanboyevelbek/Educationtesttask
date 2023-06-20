using Educationtesttask.Domain.Exceptions.Students;
using Educationtesttask.Domain.Exceptions.Teachers;
using FluentValidation.Results;

namespace Educationtesttask.Application.Services
{
	public partial class StudentService
	{
		private static void Validate(ValidationResult validationResult)
		{
			var invalidStudentException = new InvalidStudentException();

			foreach (var error in validationResult.Errors)
			{
				invalidStudentException.UpsertDataList(error.PropertyName, error.ErrorMessage);
			}

			invalidStudentException.ThrowIfContainsErrors();
		}
	}
}
