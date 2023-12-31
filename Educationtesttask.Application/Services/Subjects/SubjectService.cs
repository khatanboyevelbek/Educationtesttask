﻿using System.Linq.Expressions;
using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.Logging;
using Educationtesttask.Application.Validations.Subjects;
using Educationtesttask.Domain.DTOs.Subjects;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Domain.Entities.Account;
using Educationtesttask.Domain.Enums;
using Educationtesttask.Domain.Exceptions.Subjects;
using Educationtesttask.Infrastructure.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Data.SqlClient;

namespace Educationtesttask.Application.Services.Subjects
{
	public partial class SubjectService : ISubjectService
	{
		private readonly ISubjectRepository subjectRepository;
		private readonly ISerilogLogger logger;
		private readonly IValidator<SubjectCreateDto> validatorCreate;
		private readonly IValidator<SubjectUpdateDto> validatorUpdate;
		private readonly IHttpContextCurrentUserProvider httpContextCurrentUserProvider;

		public SubjectService(ISubjectRepository subjectRepository, ISerilogLogger logger,
            IValidator<SubjectCreateDto> validatorCreate,
            IValidator<SubjectUpdateDto> validatorUpdate,
			IHttpContextCurrentUserProvider httpContextCurrentUserProvider)
		{
			this.subjectRepository = subjectRepository;
			this.logger = logger;
			this.validatorCreate = validatorCreate;
			this.validatorUpdate = validatorUpdate;
			this.httpContextCurrentUserProvider = httpContextCurrentUserProvider;
		}

		public async Task<Subject> AddAsync(SubjectCreateDto viewModel)
		{
			try
			{
				UserClaims currentUser = this.httpContextCurrentUserProvider.GetCurrentUser();

				if (currentUser.Role != Role.Teacher)
				{
					throw new RestrictedAccessSubjectException();
				}

				if (viewModel is null)
				{
					throw new NullSubjectException();
				}

				ValidationResult validationResult = await this.validatorCreate.ValidateAsync(viewModel);
				Validate(validationResult);

				bool existingSubject = 
					this.subjectRepository.SelectAllAsync().Any(s => s.Name.ToLower() == viewModel.Name.ToLower());

				if (existingSubject)
				{
					throw new AlreadyExistSubjectException();
				}

				var subject = new Subject()
				{
					Id = Guid.NewGuid(),
					Name = viewModel.Name,
					TeacherId = currentUser.UserId,
					CreatedDate = DateTimeOffset.Now,
				    UpdatedDate = DateTimeOffset.Now
				};

				Subject savedSubject = await this.subjectRepository.AddAsync(subject);

				return savedSubject;
			}
			catch (NullSubjectException nullSubjectException)
			{
				this.logger.LogError(nullSubjectException);

				throw new SubjectValidationException(nullSubjectException);
			}
			catch (InvalidSubjectException  invalidSubjectException)
			{
				this.logger.LogError(invalidSubjectException);

				throw new SubjectValidationException(invalidSubjectException);
			}
			catch (AlreadyExistSubjectException alreadyExistSubjectException)
			{
				this.logger.LogError(alreadyExistSubjectException);

				throw new SubjectDependencyException(alreadyExistSubjectException);
			}
			catch (RestrictedAccessSubjectException restrictedAccessSubjectException)
			{
				this.logger.LogError(restrictedAccessSubjectException);

				throw new SubjectDependencyException(restrictedAccessSubjectException);
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedSubjectStorageException(sqlException);
			}
			catch(Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedSubjectServiceException(exception);
			}
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			try
			{
				UserClaims currentUser = this.httpContextCurrentUserProvider.GetCurrentUser();

				if (currentUser.Role != Role.Teacher)
				{
					throw new RestrictedAccessSubjectException();
				}

				var existingEntity = await this.subjectRepository.SelectByIdAsync(id);

				if (existingEntity is null)
				{
					throw new SubjectNotFoundException();
				}

				return await this.subjectRepository.DeleteAsync(existingEntity);
			}
			catch (SubjectNotFoundException subjectNotFoundException)
			{
				this.logger.LogError(subjectNotFoundException);

				throw new SubjectDependencyException(subjectNotFoundException);
			}
			catch (RestrictedAccessSubjectException restrictedAccessSubjectException)
			{
				this.logger.LogError(restrictedAccessSubjectException);

				throw new SubjectDependencyException(restrictedAccessSubjectException);
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedSubjectStorageException(sqlException);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedSubjectServiceException(exception);
			}
		}

		public async Task<Subject> ModifyAsync(SubjectUpdateDto viewModel)
		{
			try
			{
				UserClaims currentUser = this.httpContextCurrentUserProvider.GetCurrentUser();

				if (currentUser.Role != Role.Teacher)
				{
					throw new RestrictedAccessSubjectException();
				}

				if (viewModel is null)
				{
					throw new NullSubjectException();
				}

				ValidationResult validationResult = await this.validatorUpdate.ValidateAsync(viewModel);
				Validate(validationResult);

				Subject existingSubject = await this.subjectRepository.SelectByIdAsync(viewModel.Id);

				if (existingSubject is null)
				{
					throw new SubjectNotFoundException();
				}

				existingSubject.Name = viewModel.Name;
				existingSubject.UpdatedDate = DateTimeOffset.Now;

				return await this.subjectRepository.UpdateAsync(existingSubject);
			}
			catch (NullSubjectException nullSubjectException)
			{
				this.logger.LogError(nullSubjectException);

				throw new SubjectValidationException(nullSubjectException);
			}
			catch (InvalidSubjectException invalidSubjectException)
			{
				this.logger.LogError(invalidSubjectException);

				throw new SubjectValidationException(invalidSubjectException);
			}
			catch (SubjectNotFoundException subjectNotFoundException)
			{
				this.logger.LogError(subjectNotFoundException);

				throw new SubjectDependencyException(subjectNotFoundException);
			}
			catch (RestrictedAccessSubjectException restrictedAccessSubjectException)
			{
				this.logger.LogError(restrictedAccessSubjectException);

				throw new SubjectDependencyException(restrictedAccessSubjectException);
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedSubjectStorageException(sqlException);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedSubjectServiceException(exception);
			}
		}

		public IQueryable<Subject> RetrieveAll(Expression<Func<Subject, bool>> filter = null)
		{
			try
			{
				return this.subjectRepository.SelectAllAsync(filter);
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedSubjectStorageException(sqlException);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedSubjectServiceException(exception);
			}
		}

		public async Task<Subject> RetrieveByIdAsync(Guid id)
		{
			try
			{
				var retrieveSubject = await this.subjectRepository.SelectByIdAsync(id);

				if (retrieveSubject is null)
				{
					throw new SubjectNotFoundException();
				}

				return retrieveSubject;
			}
			catch (SubjectNotFoundException subjectNotFoundException)
			{
				this.logger.LogError(subjectNotFoundException);

				throw new SubjectDependencyException(subjectNotFoundException);
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedSubjectStorageException(sqlException);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedSubjectServiceException(exception);
			}
		}

		public async Task<Subject> RetrieveSubjectThatHasHighestAvarageGrade()
		{
			try
			{
				Subject subject = this.subjectRepository.SelectAllAsync().OrderByDescending(s =>
					s.StudentSubjects.Average(ss => ss.Grade)).FirstOrDefault();

				return subject;
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedSubjectStorageException(sqlException);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedSubjectServiceException(exception);
			}
		}
	}
}
