using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Students
{
	public class RestrictAccessStudentException : Xeption
	{
		public RestrictAccessStudentException()
			: base(message: "You have not access for student")
		{ }
	}
}
