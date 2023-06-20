using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Students
{
	public class NullStudentException : Xeption
	{
		public NullStudentException()
			: base(message: "Student is null")
		{ }
	}
}
