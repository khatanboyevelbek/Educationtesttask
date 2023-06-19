using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Teachers
{
	public class FailedTeacherStorageException : Xeption
	{
		public FailedTeacherStorageException(Exception innerException)
			: base(message: "Failed teacher storage error occured. Contact support", 
				  innerException)
		{ }
	}
}
