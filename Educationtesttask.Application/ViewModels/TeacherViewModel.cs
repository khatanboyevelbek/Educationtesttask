using Educationtesttask.Domain.Entities;

namespace Educationtesttask.Application.ViewModels
{
	public class TeacherViewModel
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public DateTime BirthDate { get; set; }
	}
}
