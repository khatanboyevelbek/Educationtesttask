using System.Linq.Expressions;
using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.Logging;
using Educationtesttask.Application.Validations.StudentSubjects;
using Educationtesttask.Application.ViewModels.StudentSubjects;
using Educationtesttask.Domain.Entities;
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

		public StudentSubjectService(IStudentSubjectRepository studentSubjectRepository, 
			ISerilogLogger logger, StudentSubjectViewModelValidation validator)
		{
			this.studentSubjectRepository = studentSubjectRepository;
			this.logger = logger;
			this.validator = validator;
		}

		public async Task<StudentSubject> AddAsync(StudentSubjectViewModel viewModel)
		{
			try
			{
				if (viewModel is null)
				{
					throw new NullStudentSubjectException();
				}

				ValidationResult validationResult = validator.Validate(viewModel);
				Validate(validationResult);

				var studentSubject = new StudentSubject()
				{
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

		public async Task<StudentSubject> ModifyAsync(StudentSubjectViewModel viewModel)
		{
			try
			{
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

		public IQueryable<StudentSubject> RetrieveAll(Expression<Func<StudentSubject, bool>> filter = null)
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
