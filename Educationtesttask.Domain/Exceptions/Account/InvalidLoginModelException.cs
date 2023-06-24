using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Account
{
	public class InvalidLoginModelException : Xeption
	{
		public InvalidLoginModelException()
			: base(message: "Login cridentials are invalid")
		{}
	}
}
