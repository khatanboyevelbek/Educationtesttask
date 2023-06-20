using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Students
{
	public class StudentNotFoundException : Xeption
	{
		public StudentNotFoundException()
			: base(message: "Student not found with this id")
		{ }
	}
}
