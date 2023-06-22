using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.StudentSubjects
{
	public class FailedStudentSubjectServiceException : Xeption
	{
		public FailedStudentSubjectServiceException(Exception innerException) 
			: base(message: "Unexpected StudentSubject error occured. Contact support", 
				  innerException)
		{ }
	}
}
