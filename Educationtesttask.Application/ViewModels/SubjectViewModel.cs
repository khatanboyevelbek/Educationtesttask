using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educationtesttask.Application.ViewModels
{
	public class SubjectViewModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid StudentId { get; set; }
		public Guid TeacherId { get; set; }
	}
}
