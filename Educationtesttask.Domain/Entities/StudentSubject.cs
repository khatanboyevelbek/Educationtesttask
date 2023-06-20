using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educationtesttask.Domain.Entities
{
	public class StudentSubject
	{
		public Guid StudentId { get; set; }
		public Student Student { get; set; }

		public Guid SubjectId { get; set; }
		public Subject Subject { get; set; }

		public int Grade { get; set; }
	}
}
