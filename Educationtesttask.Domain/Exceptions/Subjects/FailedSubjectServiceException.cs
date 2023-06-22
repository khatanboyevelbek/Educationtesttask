using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Subjects
{
	public class FailedSubjectServiceException : Xeption
	{
		public FailedSubjectServiceException(Exception innerException) 
			: base(message: "Unexpected error of subject service occured. Contact support",
				  innerException)
		{ }
	}
}
