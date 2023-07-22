using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Domain.DTOs.Subjects;
using Educationtesttask.Domain.Entities;

namespace Educationtesttask.Application.Interfaces
{
	public interface ISubjectService 
		: IGenericService<Subject, SubjectCreateDto, SubjectUpdateDto>
	{
		Task<Subject> RetrieveSubjectThatHasHighestAvarageGrade();
	}
}
