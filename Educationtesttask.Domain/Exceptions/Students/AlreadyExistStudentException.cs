using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Students
{
	public class AlreadyExistStudentException : Xeption
	{
		public AlreadyExistStudentException()
			: base(message: "Student is alreay exist. Please try again")
		{ }
	}
}
