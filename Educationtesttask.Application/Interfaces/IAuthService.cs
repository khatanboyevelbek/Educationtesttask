using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Domain.Entities.Account;

namespace Educationtesttask.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginSuccessResponse> LoginStudent(LoginModel loginModel);
        Task<LoginSuccessResponse> LoginTeacher(LoginModel loginModel);
    }
}
