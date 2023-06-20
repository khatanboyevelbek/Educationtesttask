using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Students
{
	public class StudentDependencyException : Xeption
	{
		public StudentDependencyException(Xeption innerException) 
			: base(message: "Student dependency error occured. Try again", 
				  innerException)
		{ }
	}
}
