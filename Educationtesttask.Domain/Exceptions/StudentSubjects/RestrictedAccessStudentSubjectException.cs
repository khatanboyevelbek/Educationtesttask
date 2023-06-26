using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.StudentSubjects
{
	public class RestrictedAccessStudentSubjectException : Xeption
	{
		public RestrictedAccessStudentSubjectException()
			: base(message: "You have not access for subject of student")
		{ }
	}
}
