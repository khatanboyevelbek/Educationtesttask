using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Application.ViewModels.Subjects;
using Educationtesttask.Domain.Entities;

namespace Educationtesttask.Application.Interfaces
{
	public interface ISubjectService 
		: IGenericService<Subject, SubjectCreateViewModel, SubjectUpdateViewModel>
	{
	}
}
