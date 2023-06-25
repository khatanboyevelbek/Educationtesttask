using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Domain.Enums;

namespace Educationtesttask.Domain.Entities.Account
{
	public class UserClaims
	{
		public Guid UserId { get; set; }
		public string Email { get; set; }
        public Role Role { get; set; }
	}
}
