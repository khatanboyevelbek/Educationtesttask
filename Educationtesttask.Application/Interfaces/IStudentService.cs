using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Application.ViewModels.Students;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Domain.Enums;

namespace Educationtesttask.Application.Interfaces
{
    public interface IStudentService 
		: IGenericService<Student, StudentCreateViewModel, StudentUpdateViewModel>
	{
		IQueryable<Student> RetrieveAllFileteredByBirthDate(int startMonth, int startDay, int endMonth, int endDay);
		IQueryable<Student> RetrieveAllFilteredByMobileOperators(MobileOperators mobileOperators);
	}
}
