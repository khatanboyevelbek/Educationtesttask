using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Subjects
{
	public class AlreadyExistSubjectException : Xeption
	{
		public AlreadyExistSubjectException() 
			: base(message: " Subject is already exist with this name")
		{ }
	}
}
