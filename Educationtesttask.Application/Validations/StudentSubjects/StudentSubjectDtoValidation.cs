using Educationtesttask.Domain.DTOs.StudentSubjects;
using Educationtesttask.Infrastructure.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Educationtesttask.Application.Validations.StudentSubjects
{
    public class StudentSubjectDtoValidation : AbstractValidator<StudentSubjectDto>
	{
		public StudentSubjectDtoValidation(ISubjectRepository subjectRepository)
		{
			RuleFor(s => s.SubjectId)
				.NotEmpty()
				.MustAsync(async (request, id, cancellationToken) =>
					await subjectRepository.SelectAllAsync().AnyAsync(s => s.Id == request.SubjectId))
				.WithMessage("Subject with subjectId is not found in the system");

			RuleFor(s => s.Grade)
				.NotEmpty()
				.GreaterThanOrEqualTo(1)
				.LessThanOrEqualTo(100);
		}
	}
}
