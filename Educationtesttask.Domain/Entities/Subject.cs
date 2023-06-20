using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educationtesttask.Domain.Entities
{
	public class Subject
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

		public Guid TeacherId { get; set; }
		public Teacher Teacher { get; set; }

		public ICollection<StudentSubject> StudentSubjects { get; set; }

		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset UpdatedDate { get; set; }
	}
}
