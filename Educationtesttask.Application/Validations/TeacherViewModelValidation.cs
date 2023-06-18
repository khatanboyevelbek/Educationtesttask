using Educationtesttask.Application.ViewModels;
using FluentValidation;

namespace Educationtesttask.Application.Validations
{
	public class TeacherViewModelValidation : AbstractValidator<TeacherViewModel>
	{
		public TeacherViewModelValidation() 
		{
			RuleFor(tvm => tvm.FirstName).NotNull().NotEmpty();
			RuleFor(tvm => tvm.LastName).NotNull().NotEmpty();
			RuleFor(tvm => tvm.PhoneNumber).NotNull().NotEmpty().Must(n => n.StartsWith("+998"));
			RuleFor(tvm => tvm.Email).NotNull().NotEmpty().EmailAddress();
			RuleFor(tvm => tvm.BirthDate).NotNull().NotEmpty();
		}
	}
}
