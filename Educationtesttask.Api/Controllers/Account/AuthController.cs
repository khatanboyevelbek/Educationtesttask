using Educationtesttask.Application.Interfaces;
using Educationtesttask.Domain.Entities.Account;
using Educationtesttask.Domain.Exceptions.Account;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace Educationtesttask.Api.Controllers.Account
{
    [ApiController]
	[Route("/api/[controller]")]
	public class AuthController : RESTFulController
	{
		private readonly IAuthService authService;

		public AuthController(IAuthService authService) 
		{ 
			this.authService = authService;
		}

		[HttpPost]
		[Route("student/login")]
		public async Task<ActionResult> PostLoginStudent(LoginModel loginModel)
		{
			try
			{
				LoginSuccessResponse result = await this.authService.LoginStudent(loginModel);

				return Ok(result);
			}
			catch (LoginModelValidationException exception)
			{
				return BadRequest(exception.InnerException);
			}
			catch (LoginModelDependencyValidationException exception) 
				when (exception.InnerException is LoginModelUnauthorizedException)
			{
				return Unauthorized(exception.InnerException);
			}
			catch (FailedLoginModelStorageException exception)
			{
				return InternalServerError(exception.InnerException);	
			}
			catch (FailedLoginModelServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}
	}
}
