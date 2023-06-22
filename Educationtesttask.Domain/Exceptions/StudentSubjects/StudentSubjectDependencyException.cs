using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.StudentSubjects
{
	public class StudentSubjectDependencyException : Xeption
	{
		public StudentSubjectDependencyException(Xeption innerException) 
			: base(message: "StudentSubject dependency error occured. Try again", 
				  innerException)
		{ }
	}
}
