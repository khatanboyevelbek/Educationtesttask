namespace Educationtesttask.Domain.Entities
{
	public class Student
	{
        public Guid Id { get; set; }
        public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public DateTime BirthDate { get; set; }
		public string StudentRegNumber { get; set; }

		public virtual ICollection<StudentSubject> StudentSubjects { get; set; }

		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset UpdatedDate { get; set; }
	}
}
