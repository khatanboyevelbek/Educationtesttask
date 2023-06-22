using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Subjects
{
	public class SubjectDependencyException : Xeption
	{
		public SubjectDependencyException(Xeption innerException) 
			: base(message: "Subject dependency error occured. Try again", 
				  innerException)
		{ }
	}
}
