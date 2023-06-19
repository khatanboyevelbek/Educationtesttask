namespace Educationtesttask.Domain.Entities
{
	public class Grade
	{
		public Guid Id { get; set; }

		public Guid SubjectId { get; set; }
		public Subject Subject { get; set; }

		public Guid StudentId { get; set; }
		public Student Student { get; set; }

		public int Value { get; set; }

		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset UpdatedDate { get; set; }

		public override string ToString()
		{
			return $"{Id} - {Value}";
		}
	}
}
