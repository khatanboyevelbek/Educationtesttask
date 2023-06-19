using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Teachers
{
	public class TeacherDependencyException : Xeption
	{
		public TeacherDependencyException(Xeption innerException)
			: base(message: "Teacher dependency error occured. Contact support", 
				  innerException)
		{ }
	}
}
