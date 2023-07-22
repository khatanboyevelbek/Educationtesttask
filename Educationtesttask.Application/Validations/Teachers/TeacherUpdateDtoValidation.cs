using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Domain.DTOs.Teachers;
using Educationtesttask.Infrastructure.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Educationtesttask.Application.Validations.Teachers
{
	public class TeacherUpdateDtoValidation : AbstractValidator<TeacherUpdateDto>
	{
		public TeacherUpdateDtoValidation(ITeacherRepository teacherRepository)
		{
			RuleFor(tvm => tvm.Id)
				.NotEmpty()
				.MustAsync(async (request, id, cancellationToken) =>
					await teacherRepository.SelectAllAsync().AnyAsync(t => t.Id == request.Id))
				.WithMessage("Please provide the Id of an existing teacher");

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
