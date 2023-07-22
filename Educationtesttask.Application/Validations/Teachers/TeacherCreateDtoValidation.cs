using Educationtesttask.Domain.DTOs.Teachers;
using FluentValidation;

namespace Educationtesttask.Application.Validations.Teachers
{
    public class TeacherCreateDtoValidation : AbstractValidator<TeacherCreateDto>
    {
        public TeacherCreateDtoValidation()
        {
            RuleFor(tvm => tvm.FirstName)
                .NotNull()
                .NotEmpty();

            RuleFor(tvm => tvm.LastName)
                .NotNull()
                .NotEmpty();

            RuleFor(tvm => tvm.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .Must(n => n.StartsWith("+998"))
                .WithMessage("Phone number should start with '+998'");

            RuleFor(tvm => tvm.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

			RuleFor(s => s.Password)
                .NotNull()
                .Must(p => p.Length >= 8)
				.WithMessage("Password should contain at least 8 characters");

            RuleFor(tvm => tvm.BirthDate)
                .NotEmpty()
                .LessThan(DateTime.Today);
        }
    }
}
