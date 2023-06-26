using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Teachers
{
	public class RestrictAccessTeacherException : Xeption
	{
		public RestrictAccessTeacherException()
			: base(message: "You have not access for teacher")
		{ }
	}
}
