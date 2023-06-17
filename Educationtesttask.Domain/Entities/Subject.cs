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

		public Guid StudentId { get; set; }
		public Student Student { get; set; }

		public Guid TeacharId { get; set; }
		public Teacher Teacher { get; set; }

		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset UpdatedDate { get; set; }

		public override string ToString()
		{
			return $"{Id} - {Name}";
		}
	}
}
