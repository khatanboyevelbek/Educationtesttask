using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace Educationtesttask.Domain.Exceptions.Account
{
	public class FailedLoginModelServiceException : Xeption
	{
		public FailedLoginModelServiceException(Exception exception)
			: base(message: "Unexpected loginModel error occured. Contact support",
				  exception)
		{}
	}
}
