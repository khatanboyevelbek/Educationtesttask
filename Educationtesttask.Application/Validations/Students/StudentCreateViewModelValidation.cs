using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Application.ViewModels.Students;
using FluentValidation;

namespace Educationtesttask.Application.Validations.Students
{
    public class StudentCreateViewModelValidation : AbstractValidator<StudentCreateViewModel>
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

            RuleFor(s => s.BirthDate).NotNull().NotEmpty()
                .WithMessage("Please provide a valid birth date");

            RuleFor(s => s.StudentRegNumber).NotNull().NotEmpty()
                .WithMessage("Please provide a valid student reg number");
        }
    }
}
