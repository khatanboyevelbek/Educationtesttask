using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Domain.Entities.Account;
using FluentValidation;

namespace Educationtesttask.Application.Validations.Account
{
    public class LoginModelValidation : AbstractValidator<LoginModel>
	{
		public LoginModelValidation() 
		{
			RuleFor(s => s.Email).NotNull().NotEmpty().EmailAddress()
				.WithMessage("Please provide a valid email address");

			RuleFor(s => s.Password).NotNull().Must(p => p.Length >= 8)
				.WithMessage("Password should be minumum 8 characters");
		}
	}
}
