using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Subjects
{
	public class FailedSubjectStorageException : Xeption
	{
		public FailedSubjectStorageException(Exception innerException)
			: base(message: "Failed subject storage error occured. Contact support", 
				  innerException)
		{ }
	}
}
