using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Students
{
	public class StudentValidationException : Xeption
	{
		public StudentValidationException(Xeption innerException)
			: base(message: "Student validation error occured. Fix it and try again", 
				  innerException)
		{ }
	}
}
