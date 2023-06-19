using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Teachers
{
	public class TeacherValidationException : Xeption
	{
		public TeacherValidationException(Xeption innerException)
			: base(message: "Teacher validation error occured. Fix it and try again", 
				  innerException)
		{ }
	}
}
