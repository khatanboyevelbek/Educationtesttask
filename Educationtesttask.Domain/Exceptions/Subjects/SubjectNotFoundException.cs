using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Subjects
{
	public class SubjectNotFoundException : Xeption
	{
		public SubjectNotFoundException() 
			: base(message: "Subject not found with this id")
		{ }
	}
}
