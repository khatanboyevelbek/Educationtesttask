using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.Logging;
using Educationtesttask.Application.Validations.Account;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Domain.Entities.Account;
using Educationtesttask.Domain.Exceptions.Account;
using Educationtesttask.Infrastructure.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Educationtesttask.Application.Services.Account
{
    public partial class AuthService : IAuthService
	{
		private readonly IStudentRepository studentRepository;
		private readonly ITeacherRepository teacherRepository;
		private readonly IAuthManager authManager;
		private readonly ISecurityPassword securityPassword;
		private readonly IValidator<LoginModel> validator;
		private readonly ISerilogLogger logger;

		public AuthService(IStudentRepository studentRepository, 
			ITeacherRepository teacherRepository, IAuthManager authManager, 
			ISecurityPassword securityPassword, LoginModelValidation validator,
			ISerilogLogger logger)
		{
			this.studentRepository = studentRepository;
			this.teacherRepository = teacherRepository;
			this.authManager = authManager;
			this.securityPassword = securityPassword;
			this.validator = validator;
			this.logger = logger;
		}

		public async Task<LoginSuccessResponse> LoginStudent(LoginModel loginModel)
		{
			try
			{
				if (loginModel is null)
				{
					throw new NullLoginModelException();
				}

				ValidationResult validationResult = this.validator.Validate(loginModel);
				Validate(validationResult);

				Student existingStudent = await this.studentRepository.SelectStudentByEmail(loginModel.Email);
				bool verifyPassword = this.securityPassword.Verify(loginModel.Password, existingStudent.Password);

				if ((existingStudent is not null) && verifyPassword)
				{
					string token = this.authManager.GenerateToken(existingStudent);

					var loginSuccessResponse = new LoginSuccessResponse()
					{
						Email = existingStudent.Email,
						Token = token
					};

					return loginSuccessResponse;
				}

				throw new LoginModelUnauthorizedException();
			}
			catch (NullLoginModelException exception)
			{
				this.logger.LogError(exception);

				throw new LoginModelValidationException(exception);
			}
			catch (InvalidLoginModelException exception)
			{
				this.logger.LogError(exception);

				throw new LoginModelValidationException(exception);
			}
			catch (LoginModelUnauthorizedException exception)
			{
				this.logger.LogError(exception);

				throw new LoginModelDependencyValidationException(exception);
			}
			catch (SqlException exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedLoginModelStorageException(exception);
			}
			catch (Exception exception) 
			{ 
				this.logger.LogCritical(exception);

				throw new FailedLoginModelServiceException(exception);
			}
		}

		public async Task<LoginSuccessResponse> LoginTeacher(LoginModel loginModel)
		{
			try
			{
				if (loginModel is null)
				{
					throw new NullLoginModelException();
				}

				ValidationResult validationResult = this.validator.Validate(loginModel);
				Validate(validationResult);

				Teacher existingTeacher = await this.teacherRepository.SelectTeacherByEmail(loginModel.Email);
				bool verifyPassword = this.securityPassword.Verify(loginModel.Password, existingTeacher.Password);

				if ((existingTeacher is not null) && verifyPassword)
				{
					string token = this.authManager.GenerateToken(existingTeacher);

					var loginSuccessResponse = new LoginSuccessResponse()
					{
						Email = existingTeacher.Email,
						Token = token
					};

					return loginSuccessResponse;
				}

				throw new LoginModelUnauthorizedException();
			}
			catch (NullLoginModelException exception)
			{
				this.logger.LogError(exception);

				throw new LoginModelValidationException(exception);
			}
			catch (InvalidLoginModelException exception)
			{
				this.logger.LogError(exception);

				throw new LoginModelValidationException(exception);
			}
			catch (LoginModelUnauthorizedException exception)
			{
				this.logger.LogError(exception);

				throw new LoginModelDependencyValidationException(exception);
			}
			catch (SqlException exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedLoginModelStorageException(exception);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedLoginModelServiceException(exception);
			}
		}
	}
}
