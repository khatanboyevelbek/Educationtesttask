using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Students
{
	public class FailedStudentStorageException : Xeption
	{
		public FailedStudentStorageException(Exception innerException) 
			: base(message: "Failed student storage error occured. Contact support", 
				  innerException)
		{ }
	}
}
