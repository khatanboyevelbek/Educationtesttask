using Educationtesttask.Domain.DTOs.Students;
using Educationtesttask.Infrastructure.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Educationtesttask.Application.Validations.Students
{
    public class StudentUpdateDtoValidation : AbstractValidator<StudentUpdateDto>
	{
		public StudentUpdateDtoValidation(IStudentRepository studentRepository)
		{
			RuleFor(s => s.Id)
				.NotEmpty()
				.MustAsync(async (request, id, token) =>
					await studentRepository.SelectAllAsync().AnyAsync(s => s.Id == request.Id, token))
				.WithMessage("Student with this id is not found");

			RuleFor(s => s.FirstName)
				.NotNull()
				.NotEmpty();

			RuleFor(s => s.LastName)
				.NotNull()
				.NotEmpty();

			RuleFor(s => s.PhoneNumber)
				.NotNull()
				.NotEmpty()
				.Must(n => n.StartsWith("+998"))
                .WithMessage("Phone number should start with '998'");

            RuleFor(s => s.Email)
				.NotNull()
				.NotEmpty()
				.EmailAddress();

			RuleFor(s => s.Password)
				.NotNull()
				.Must(p => p.Length >= 8)
                .WithMessage("Password should contain at least 8 characters");

            RuleFor(s => s.BirthDate)
				.NotEmpty()
                .LessThan(DateTime.Today);

            RuleFor(s => s.StudentRegNumber)
				.NotNull()
				.NotEmpty();
		}
	}
}
