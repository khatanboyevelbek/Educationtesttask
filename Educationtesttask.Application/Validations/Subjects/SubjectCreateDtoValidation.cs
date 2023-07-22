using Educationtesttask.Domain.DTOs.Subjects;
using FluentValidation;

namespace Educationtesttask.Application.Validations.Subjects
{
    public class SubjectCreateDtoValidation : AbstractValidator<SubjectCreateDto>
	{
		public SubjectCreateDtoValidation()
		{
			RuleFor(s => s.Name).NotNull().NotEmpty()
			   .WithMessage("Please provide a valid subject name");
		}
	}
}
