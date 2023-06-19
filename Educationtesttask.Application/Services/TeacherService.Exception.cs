using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Domain.Exceptions;
using FluentValidation.Results;

namespace Educationtesttask.Application.Services
{
	public partial class TeacherService
	{
		private static void Validate(ValidationResult validationResult)
		{
			var invalidTeacherException = new InvalidTeacherException();

			foreach (var error in validationResult.Errors)
			{
				invalidTeacherException.UpsertDataList(error.PropertyName, error.ErrorMessage);
			}

			invalidTeacherException.ThrowIfContainsErrors();
		}
	}
}
