using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Subjects
{
	public class InvalidSubjectException : Xeption
	{
		public InvalidSubjectException() 
			: base("Subject is invalid")
		{ }
	}
}
