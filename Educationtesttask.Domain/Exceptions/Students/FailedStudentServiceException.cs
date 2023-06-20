using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Students
{
	public class FailedStudentServiceException : Xeption
	{
		public FailedStudentServiceException(Exception innerException)
			: base(message: "Unexpected error of student service occured. Contact support", 
				  innerException)
		{ }
	}
}
