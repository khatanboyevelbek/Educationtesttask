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

		public virtual ICollection<Subject> Subjects { get; set; }
		public virtual ICollection<Grade> Grades { get; set; }

		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset UpdatedDate { get; set; }

		public override string ToString()
		{
			return $"{Id} - {FirstName} - {LastName} - {PhoneNumber} - {Email} - {BirthDate} - {StudentRegNumber}";
		}
	}
}
