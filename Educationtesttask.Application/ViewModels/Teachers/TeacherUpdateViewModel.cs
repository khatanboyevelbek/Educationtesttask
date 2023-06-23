using Educationtesttask.Domain.Entities;
using Educationtesttask.Domain.Enums;

namespace Educationtesttask.Application.ViewModels.Teachers
{
    public class TeacherUpdateViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
		public string Password { get; set; }
		public DateTime BirthDate { get; set; }
    }
}
