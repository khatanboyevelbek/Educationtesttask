using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Domain.Exceptions.Account;
using Educationtesttask.Domain.Exceptions.Students;
using FluentValidation.Results;

namespace Educationtesttask.Application.Services.Account
{
	public partial class AuthService
	{
		private static void Validate(ValidationResult validationResult)
		{
			var invalidLoginModelException = new InvalidLoginModelException();

			foreach (var error in validationResult.Errors)
			{
				invalidLoginModelException.UpsertDataList(error.PropertyName, error.ErrorMessage);
			}

			invalidLoginModelException.ThrowIfContainsErrors();
		}
	}
}
