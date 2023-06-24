using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Domain.Entities.Account;

namespace Educationtesttask.Application.Services.Account
{
	public interface IAuthService
	{
		Task<LoginSuccessResponse> LoginStudent(LoginModel loginModel);
		Task<LoginSuccessResponse> LoginTeacher(LoginModel loginModel);
	}
}
