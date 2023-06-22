using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.StudentSubjects
{
	public class StudentSubjectValidationException : Xeption
	{
		public StudentSubjectValidationException(Xeption innerException)
			: base(message: "StudentSubject vakidation error occured. Fix it and try again",
				  innerException)
		{ }
	}
}
