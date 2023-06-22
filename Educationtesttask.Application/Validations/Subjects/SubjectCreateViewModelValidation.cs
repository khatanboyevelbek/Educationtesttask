using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Application.ViewModels.Subjects;
using FluentValidation;

namespace Educationtesttask.Application.Validations.Subjects
{
	public class SubjectCreateViewModelValidation : AbstractValidator<SubjectCreateViewModel>
	{
		public SubjectCreateViewModelValidation()
		{
			RuleFor(s => s.Name).NotNull().NotEmpty()
			   .WithMessage("Please provide a valid subject name");
		}
	}
}
