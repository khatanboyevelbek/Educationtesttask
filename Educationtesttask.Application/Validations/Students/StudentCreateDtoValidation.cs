using Educationtesttask.Domain.DTOs.Students;
using FluentValidation;

namespace Educationtesttask.Application.Validations.Students
{
    public class StudentCreateDtoValidation : AbstractValidator<StudentCreateDto>
    {
        public StudentCreateDtoValidation()
        {
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
