using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Account
{
	public class LoginModelValidationException : Xeption
	{
		public LoginModelValidationException(Xeption innerException) 
			: base(message: "LoginModel validation error occured. Fix it and try again", 
				  innerException)
		{ }
	}
}
