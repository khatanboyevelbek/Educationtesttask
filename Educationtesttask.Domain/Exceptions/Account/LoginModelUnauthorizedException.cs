using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Account
{
	public class LoginModelUnauthorizedException : Xeption
	{
		public LoginModelUnauthorizedException() 
			: base(message: "Unauthorized error occured")
		{ }
	}
}
