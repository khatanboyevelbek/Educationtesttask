using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Subjects
{
	public class SubjectValidationException : Xeption
	{
		public SubjectValidationException(Xeption innerException)
			: base(message: "Subject validation error occured. Fix it and try again",
				  innerException)
		{ }
	}
}
