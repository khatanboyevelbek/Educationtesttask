using System.Linq.Expressions;
using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.Logging;
using Educationtesttask.Application.Validations.StudentSubjects;
using Educationtesttask.Domain.DTOs.StudentSubjects;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Domain.Entities.Account;
using Educationtesttask.Domain.Enums;
using Educationtesttask.Domain.Exceptions.StudentSubjects;
using Educationtesttask.Infrastructure.Interfaces;
using FluentValidation.Results;
using Microsoft.Data.SqlClient;

namespace Educationtesttask.Application.Services.StudentSubjects
{
	public partial class StudentSubjectService : IStudentSubjectService
	{
		private readonly IStudentSubjectRepository studentSubjectRepository;
		private readonly ISerilogLogger logger;
		private readonly StudentSubjectViewModelValidation validator;
		private readonly IHttpContextCurrentUserProvider httpContextCurrentUserProvider;

		public StudentSubjectService(IStudentSubjectRepository studentSubjectRepository, 
			ISerilogLogger logger, StudentSubjectViewModelValidation validator,
			IHttpContextCurrentUserProvider httpContextCurrentUserProvider)
		{
			this.studentSubjectRepository = studentSubjectRepository;
			this.logger = logger;
			this.validator = validator;
			this.validator = validator;
			this.httpContextCurrentUserProvider = httpContextCurrentUserProvider;
		}

		public async Task<StudentSubject> AddAsync(StudentSubjectDto viewModel)
		{
			try
			{
				UserClaims currentUser = this.httpContextCurrentUserProvider.GetCurrentUser();

				if (currentUser.Role != Role.Student)
				{
					throw new RestrictedAccessStudentSubjectException();
				}

				if (viewModel is null)
				{
					throw new NullStudentSubjectException();
				}

				ValidationResult validationResult = validator.Validate(viewModel);
				Validate(validationResult);

				var studentSubject = new StudentSubject()
				{
					StudentId = currentUser.UserId,
					SubjectId = viewModel.SubjectId,
					Grade = viewModel.Grade
				};

				return await this.studentSubjectRepository.AddAsync(studentSubject);
			}
			catch (NullStudentSubjectException nullStudentSubjectException)
			{
				this.logger.LogError(nullStudentSubjectException);

				throw new StudentSubjectValidationException(nullStudentSubjectException);
			}
			catch (InvalidStudentSubjectException invalidStudentSubjectException)
			{
				this.logger.LogError(invalidStudentSubjectException);

				throw new StudentSubjectValidationException(invalidStudentSubjectException);
			}
			catch (RestrictedAccessStudentSubjectException restrictedAccessStudentSubjectException)
			{
				this.logger.LogError(restrictedAccessStudentSubjectException);

				throw new StudentSubjectDependencyException(restrictedAccessStudentSubjectException);
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedStudentSubjectStorageException(sqlException);
			}
			catch(Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedStudentSubjectServiceException(exception);
			}
		}

		public async Task<bool> DeleteAsync(Guid subjectId, Guid studentId)
		{
			try
			{
				UserClaims currentUser = this.httpContextCurrentUserProvider.GetCurrentUser();

				if (currentUser.Role != Role.Student)
				{
					throw new RestrictedAccessStudentSubjectException();
				}

				StudentSubject existingEntity = await this.studentSubjectRepository.SelectByIdAsync(subjectId, studentId);

				if (existingEntity is null)
				{
					throw new StudentSubjectNotFoundException();
				}

				return await this.studentSubjectRepository.DeleteAsync(existingEntity);
			}
			catch (StudentSubjectNotFoundException studentSubjectNotFoundException)
			{
				this.logger.LogError(studentSubjectNotFoundException);

				throw new StudentSubjectDependencyException(studentSubjectNotFoundException);
			}
			catch (RestrictedAccessStudentSubjectException restrictedAccessStudentSubjectException)
			{
				this.logger.LogError(restrictedAccessStudentSubjectException);

				throw new StudentSubjectDependencyException(restrictedAccessStudentSubjectException);
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedStudentSubjectStorageException(sqlException);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedStudentSubjectServiceException(exception);
			}
		}

		public async Task<StudentSubject> ModifyAsync(StudentSubjectDto viewModel)
		{
			try
			{
				UserClaims currentUser = this.httpContextCurrentUserProvider.GetCurrentUser();

				if (currentUser.Role != Role.Student)
				{
					throw new RestrictedAccessStudentSubjectException();
				}

				if (viewModel is null)
				{
					throw new NullStudentSubjectException();
				}

				ValidationResult validationResult = validator.Validate(viewModel);
				Validate(validationResult);

				StudentSubject existingStudentSubject = await this.studentSubjectRepository.SelectByIdAsync(viewModel.SubjectId, Guid.NewGuid());

				if (existingStudentSubject is null)
				{
					throw new StudentSubjectNotFoundException();
				}

				existingStudentSubject.Grade = viewModel.Grade;

				return await this.studentSubjectRepository.UpdateAsync(existingStudentSubject);
			}
			catch (NullStudentSubjectException nullStudentSubjectException)
			{
				this.logger.LogError(nullStudentSubjectException);

				throw new StudentSubjectValidationException(nullStudentSubjectException);
			}
			catch (InvalidStudentSubjectException invalidStudentSubjectException)
			{
				this.logger.LogError(invalidStudentSubjectException);

				throw new StudentSubjectValidationException(invalidStudentSubjectException);
			}
			catch (StudentSubjectNotFoundException studentSubjectNotFoundException)
			{
				this.logger.LogError(studentSubjectNotFoundException);

				throw new StudentSubjectDependencyException(studentSubjectNotFoundException);
			}
			catch (RestrictedAccessStudentSubjectException restrictedAccessStudentSubjectException)
			{
				this.logger.LogError(restrictedAccessStudentSubjectException);

				throw new StudentSubjectDependencyException(restrictedAccessStudentSubjectException);
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedStudentSubjectStorageException(sqlException);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedStudentSubjectServiceException(exception);
			}
		}

		public IQueryable<StudentSubject> RetrieveAll(Expression<Func<StudentSubject, bool>> filter)
		{
			try
			{
				return this.studentSubjectRepository.SelectAllAsync(filter);
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedStudentSubjectStorageException(sqlException);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedStudentSubjectServiceException(exception);
			}
		}

		public async Task<StudentSubject> RetrieveByIdAsync(Guid subjectId, Guid studentId)
		{
			try
			{
				StudentSubject entity = await this.studentSubjectRepository.SelectByIdAsync(subjectId, studentId);

				if (entity is null)
				{
					throw new StudentSubjectNotFoundException();
				}

				return entity;
			}
			catch (StudentSubjectNotFoundException studentSubjectNotFoundException)
			{
				this.logger.LogError(studentSubjectNotFoundException);

				throw new StudentSubjectDependencyException(studentSubjectNotFoundException);
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedStudentSubjectStorageException(sqlException);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedStudentSubjectServiceException(exception);
			}
		}
	}
}
