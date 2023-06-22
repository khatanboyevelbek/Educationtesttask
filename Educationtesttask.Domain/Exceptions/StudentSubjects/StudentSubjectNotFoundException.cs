using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.StudentSubjects
{
	public class StudentSubjectNotFoundException : Xeption
	{
		public StudentSubjectNotFoundException()
			: base(message: "StudentSubject is not found")
		{ }
	}
}
