using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.StudentSubjects
{
	public class NullStudentSubjectException : Xeption
	{
		public NullStudentSubjectException() 
			: base(message: "StudentSubject is null")
		{ }
	}
}
