using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Account
{
	public class LoginModelDependencyValidationException : Xeption
	{
		public LoginModelDependencyValidationException(Xeption innerException) 
			: base(message: "User dependency validation error occred. Fix it and try again", 
				  innerException)
		{ }
	}
}
