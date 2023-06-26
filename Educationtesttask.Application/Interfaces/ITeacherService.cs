using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Application.ViewModels.Teachers;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Domain.Enums;

namespace Educationtesttask.Application.Interfaces
{
    public interface ITeacherService 
		: IGenericService<Teacher, TeacherCreateViewModel, TeacherUpdateViewModel>
	{
		IQueryable<Teacher> RetrieveAllFilteredByMobileOperators(MobileOperators mobileOperators);
	}
}
