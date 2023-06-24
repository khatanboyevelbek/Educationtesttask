using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Account
{
	public class NullLoginModelException : Xeption
	{
		public NullLoginModelException()
			: base(message: "Login cridentials are null")
		{ }
	}
}
