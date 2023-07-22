using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Domain.DTOs.Subjects;
using FluentValidation;

namespace Educationtesttask.Application.Validations.Subjects
{
	public class SubjectUpdateViewModelValidation : AbstractValidator<SubjectUpdateDto>
	{
		public SubjectUpdateViewModelValidation() 
		{
			RuleFor(s => s.Id).NotNull().NotEmpty()
			   .WithMessage("Please provide the Id of an existing subject");

			RuleFor(s => s.Name).NotNull().NotEmpty()
			   .WithMessage("Please provide a valid subject name");
		}
	}
}
