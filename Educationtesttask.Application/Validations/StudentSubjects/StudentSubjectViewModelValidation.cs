﻿using Educationtesttask.Domain.DTOs.StudentSubjects;
using FluentValidation;

namespace Educationtesttask.Application.Validations.StudentSubjects
{
    public class StudentSubjectViewModelValidation : AbstractValidator<StudentSubjectDto>
	{
		public StudentSubjectViewModelValidation()
		{
			RuleFor(s => s.SubjectId).NotNull().NotEmpty()
				.WithMessage("Please provide the Id of valid subject");

			RuleFor(s => s.Grade).NotNull().NotEmpty()
				.WithMessage("Please provide value of valid grade");
		}
	}
}
