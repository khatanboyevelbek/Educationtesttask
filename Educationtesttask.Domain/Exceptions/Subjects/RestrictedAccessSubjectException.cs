using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Subjects
{
	public class RestrictedAccessSubjectException : Xeption
	{
		public RestrictedAccessSubjectException()
			: base("You have not access for subject")
		{ }
	}
}
