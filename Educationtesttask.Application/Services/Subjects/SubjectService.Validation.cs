using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Domain.Exceptions.Students;
using Educationtesttask.Domain.Exceptions.Subjects;
using FluentValidation.Results;

namespace Educationtesttask.Application.Services.Subjects
{
	public partial class SubjectService
	{
		private static void Validate(ValidationResult validationResult)
		{
			var invalidSubjectException = new InvalidSubjectException();

			foreach (var error in validationResult.Errors)
			{
				invalidSubjectException.UpsertDataList(error.PropertyName, error.ErrorMessage);
			}

			invalidSubjectException.ThrowIfContainsErrors();
		}
	}
}
