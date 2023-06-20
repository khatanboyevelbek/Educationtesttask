using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Application.ViewModels;
using Educationtesttask.Domain.Entities;

namespace Educationtesttask.Application.Interfaces
{
	public interface IStudentService : IGenericService<Student, StudentViewModel>
	{ }
}
