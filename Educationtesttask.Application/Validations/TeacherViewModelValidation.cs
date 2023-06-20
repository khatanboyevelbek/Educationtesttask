﻿using Educationtesttask.Application.ViewModels;
using FluentValidation;

namespace Educationtesttask.Application.Validations
{
	public class TeacherViewModelValidation : AbstractValidator<TeacherViewModel>
	{
		public TeacherViewModelValidation() 
		{
			RuleFor(tvm => tvm.FirstName).NotNull().NotEmpty()
				.WithMessage("Please provide a valid firstname");

			RuleFor(tvm => tvm.LastName).NotNull().NotEmpty()
				.WithMessage("Please provide a valid lastname");

			RuleFor(tvm => tvm.PhoneNumber).NotNull().NotEmpty()
				.Must(n => n.StartsWith("+998"))
				.WithMessage("Please provide a valid phone number starting with '+998'");

			RuleFor(tvm => tvm.Email).NotNull().NotEmpty().EmailAddress()
				.WithMessage("Please provide a valid email address");

			RuleFor(tvm => tvm.BirthDate).NotNull().NotEmpty()
				.WithMessage("Please provide a valid birth date");
		}
	}
}