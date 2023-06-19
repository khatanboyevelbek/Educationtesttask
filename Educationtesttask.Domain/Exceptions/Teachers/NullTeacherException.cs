using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Teachers
{
	public class NullTeacherException : Xeption
	{
		public NullTeacherException() 
			: base(message: "Teacher is null. Fix it and try again")
		{ }	
	}
}
