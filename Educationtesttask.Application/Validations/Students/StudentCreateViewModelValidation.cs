using Educationtesttask.Domain.DTOs.Students;
using FluentValidation;

namespace Educationtesttask.Application.Validations.Students
{
    public class StudentCreateViewModelValidation : AbstractValidator<StudentCreateDto>
    {
        public StudentCreateViewModelValidation()
        {
            RuleFor(s => s.FirstName).NotNull().NotEmpty()
                .WithMessage("Please provide a valid firstname");

            RuleFor(s => s.LastName).NotNull().NotEmpty()
                .WithMessage("Please provide a valid lastname");

            RuleFor(s => s.PhoneNumber).NotNull().NotEmpty()
                .Must(n => n.StartsWith("+998"))
                .WithMessage("Please provide a valid phone number starting with '+998'");

            RuleFor(s => s.Email).NotNull().NotEmpty().EmailAddress()
                .WithMessage("Please provide a valid email address");

			RuleFor(s => s.Password).NotNull().Must(p => p.Length >= 8)
				.WithMessage("Password should be minumum 8 characters");


			RuleFor(s => s.BirthDate).NotNull().NotEmpty()
                .WithMessage("Please provide a valid birth date");

            RuleFor(s => s.StudentRegNumber).NotNull().NotEmpty()
                .WithMessage("Please provide a valid student reg number");
        }
    }
}
