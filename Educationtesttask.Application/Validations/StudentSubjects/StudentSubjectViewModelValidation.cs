using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Application.ViewModels.StudentSubjects;
using FluentValidation;

namespace Educationtesttask.Application.Validations.StudentSubjects
{
	public class StudentSubjectViewModelValidation : AbstractValidator<StudentSubjectViewModel>
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
