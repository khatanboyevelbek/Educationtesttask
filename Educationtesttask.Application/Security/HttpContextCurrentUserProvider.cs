using System.Security.Claims;
using Educationtesttask.Application.Interfaces;
using Educationtesttask.Domain.Entities.Account;
using Educationtesttask.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Educationtesttask.Application.Security
{
    public class HttpContextCurrentUserProvider : IHttpContextCurrentUserProvider
	{
		private readonly IHttpContextAccessor httpContextAccessor;

		public HttpContextCurrentUserProvider(IHttpContextAccessor httpContextAccessor)
		{
			this.httpContextAccessor = httpContextAccessor;
		}

		public UserClaims GetCurrentUser()
		{
			var identity = this.httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

			if (identity != null)
			{
				var userClaims = identity.Claims;

				return new UserClaims()
				{
					UserId = Guid.Parse(userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value),
					Email = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value,
					Role = (Role)Enum.Parse(typeof(Role), userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value)
				};
			}

			return null;
		}
	}
}
