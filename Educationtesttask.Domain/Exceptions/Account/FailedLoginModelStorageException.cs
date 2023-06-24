using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Account
{
	public class FailedLoginModelStorageException : Xeption
	{
		public FailedLoginModelStorageException(Exception innerException)
			: base(message: "Failed loginModel storage error occured. Contact support",
				  innerException)
		{}
	}
}
