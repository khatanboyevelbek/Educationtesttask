using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Teachers
{
	public class TeacherNotFoundException : Xeption
	{
		public TeacherNotFoundException()
			: base(message: "Teacher is not found with this Id")

		{ }
	}
}
