using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.StudentSubjects
{
	public class FailedStudentSubjectStorageException : Xeption
	{
		public FailedStudentSubjectStorageException(Exception innerException) 
			: base(message: "Failed studentsubject stroge error occured. Contact support", 
				  innerException)
		{ }
	}
}
