using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Students
{
	public class InvalidStudentException : Xeption
	{
		public InvalidStudentException()
			: base(message: "Student is invalid")
		{ }
	}
}
