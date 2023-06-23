using System.Linq.Expressions;
using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.Logging;
using Educationtesttask.Application.Validations.Subjects;
using Educationtesttask.Application.ViewModels.Subjects;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Domain.Exceptions.Students;
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
		private readonly SubjectCreateViewModelValidation validatorCreate;
		private readonly SubjectUpdateViewModelValidation validatorUpdate;

		public SubjectService(ISubjectRepository subjectRepository, ISerilogLogger logger, 
			SubjectCreateViewModelValidation validatorCreate, 
			SubjectUpdateViewModelValidation validatorUpdate)
		{
			this.subjectRepository = subjectRepository;
			this.logger = logger;
			this.validatorCreate = validatorCreate;
			this.validatorUpdate = validatorUpdate;
		}

		public async Task<Subject> AddAsync(SubjectCreateViewModel viewModel)
		{
			try
			{
				if (viewModel is null)
				{
					throw new NullSubjectException();
				}

				ValidationResult validationResult = validatorCreate.Validate(viewModel);
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

		public async Task<Subject> ModifyAsync(SubjectUpdateViewModel viewModel)
		{
			try
			{
				if (viewModel is null)
				{
					throw new NullSubjectException();
				}

				ValidationResult validationResult = validatorUpdate.Validate(viewModel);
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
	}
}
