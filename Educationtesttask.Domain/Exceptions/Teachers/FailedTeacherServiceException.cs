using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Teachers
{
	public class FailedTeacherServiceException : Xeption
	{
		public FailedTeacherServiceException(Exception innerException) 
			: base(message: "Unexpected error of teacher service occured", 
				  innerException) { }
	}
}
