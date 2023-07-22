using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Domain.DTOs.Subjects;
using Educationtesttask.Infrastructure.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Educationtesttask.Application.Validations.Subjects
{
	public class SubjectUpdateDtoValidation : AbstractValidator<SubjectUpdateDto>
	{
		public SubjectUpdateDtoValidation(ISubjectRepository subjectRepository) 
		{
			RuleFor(s => s.Id)
			   .NotEmpty()
			   .MustAsync(async (request, id, cancellationToken) =>
					await subjectRepository.SelectAllAsync().AnyAsync(s => s.Id == request.Id))
			   .WithMessage("Please provide the Id of an existing subject");

			RuleFor(s => s.Name).NotNull().NotEmpty()
			   .WithMessage("Please provide a valid subject name");
		}
	}
}
