using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educationtesttask.Application.ViewModels
{
	public class GradeViewModel
	{
		public Guid Id { get; set; }
		public Guid SubjectId { get; set; }
		public Guid StudentId { get; set; }
		public int Value { get; set; }
	}
}
