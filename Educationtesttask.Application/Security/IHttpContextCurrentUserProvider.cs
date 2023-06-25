using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Domain.Entities.Account;

namespace Educationtesttask.Application.Security
{
	public interface IHttpContextCurrentUserProvider
	{
		UserClaims GetCurrentUser();
	}
}
