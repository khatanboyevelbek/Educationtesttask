using System.Linq.Expressions;
using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.Logging;
using Educationtesttask.Application.Validations.Students;
using Educationtesttask.Application.ViewModels.Students;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Domain.Exceptions.Students;
using Educationtesttask.Domain.Exceptions.Teachers;
using Educationtesttask.Infrastructure.Interfaces;
using FluentValidation.Results;
using Microsoft.Data.SqlClient;

namespace Educationtesttask.Application.Services
{
    public partial class StudentService : IStudentService
	{
		private readonly IStudentRepository studentRepository;
		private readonly ISerilogLogger logger;
		private readonly StudentCreateViewModelValidation validatorCreate;
		private readonly StudentUpdateViewModelValidation validatorUpdate;

		public StudentService(IStudentRepository studentRepository, ISerilogLogger logger,
			StudentCreateViewModelValidation validatorCreate,
			StudentUpdateViewModelValidation validatorUpdate)
		{
			this.studentRepository = studentRepository;
			this.logger = logger;
			this.validatorCreate = validatorCreate;
			this.validatorUpdate = validatorUpdate;
		}

		public async Task<Student> AddAsync(StudentCreateViewModel viewModel)
		{
			try
			{
				if (viewModel is null)
				{
					throw new NullStudentException();
				}

				ValidationResult validationResult = validatorCreate.Validate(viewModel);
				Validate(validationResult);

				bool existingStudent = this.studentRepository.SelectAllAsync().Any(s => s.Email == viewModel.Email);

				if (existingStudent)
				{
					throw new AlreadyExistStudentException();
				}

				var student = new Student()
				{
					Id = Guid.NewGuid(),
					FirstName = viewModel.FirstName,
					LastName = viewModel.LastName,
					PhoneNumber = viewModel.PhoneNumber,
					Email = viewModel.Email,
					BirthDate = viewModel.BirthDate,
					StudentRegNumber = viewModel.StudentRegNumber,
					CreatedDate = DateTimeOffset.Now,
					UpdatedDate = DateTimeOffset.Now
				};

				Student addeddData = await this.studentRepository.AddAsync(student);

				return addeddData;

			}
			catch (InvalidStudentException invalidStudentException)
			{
				this.logger.LogError(invalidStudentException);

				throw new StudentValidationException(invalidStudentException);
			}
			catch (NullStudentException nullStudentException)
			{
				this.logger.LogError(nullStudentException);

				throw new StudentValidationException(nullStudentException);
			}
			catch (AlreadyExistStudentException alreadyExistStudentException)
			{
				this.logger.LogError(alreadyExistStudentException);

				throw new StudentDependencyException(alreadyExistStudentException);
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedStudentStorageException(sqlException);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedStudentServiceException(exception);
			}
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			try
			{
				Student existingEntity = await this.studentRepository.SelectByIdAsync(id);

				if (existingEntity is null)
				{
					throw new StudentNotFoundException();
				}

				return await this.studentRepository.DeleteAsync(existingEntity);
			}
			catch (StudentNotFoundException studentNotFoundException)
			{
				this.logger.LogError(studentNotFoundException);

				throw new StudentDependencyException(studentNotFoundException);
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedStudentStorageException(sqlException);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedStudentServiceException(exception);
			}
		}

		public async Task<Student> ModifyAsync(StudentUpdateViewModel viewModel)
		{
			try
			{
				if (viewModel is null)
				{
					throw new NullStudentException();
				}

				ValidationResult validationResult = validatorUpdate.Validate(viewModel);
				Validate(validationResult);

				var retrieveExistingStudent = await this.studentRepository.SelectByIdAsync(viewModel.Id);

				if (retrieveExistingStudent is null)
				{
					throw new StudentNotFoundException();
				}

				retrieveExistingStudent.FirstName = viewModel.FirstName;
				retrieveExistingStudent.LastName = viewModel.LastName;
				retrieveExistingStudent.PhoneNumber = viewModel.PhoneNumber;
				retrieveExistingStudent.Email = viewModel.Email;
				retrieveExistingStudent.BirthDate = viewModel.BirthDate;
				retrieveExistingStudent.UpdatedDate = DateTimeOffset.Now;

				return await this.studentRepository.UpdateAsync(retrieveExistingStudent);

			}
			catch (InvalidStudentException invalidStudentException)
			{
				this.logger.LogError(invalidStudentException);

				throw new StudentValidationException(invalidStudentException);
			}
			catch (NullStudentException nullStudentException)
			{
				this.logger.LogError(nullStudentException);

				throw new StudentValidationException(nullStudentException);
			}
			catch (StudentNotFoundException studentNotFoundException)
			{
				this.logger.LogError(studentNotFoundException);

				throw new StudentDependencyException(studentNotFoundException);
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedStudentStorageException(sqlException);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedStudentServiceException(exception);
			}
		}

		public IQueryable<Student> RetrieveAll(Expression<Func<Student, bool>> filter = null)
		{
			try
			{
				return this.studentRepository.SelectAllAsync(filter);
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedStudentStorageException(sqlException);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedStudentServiceException(exception);
			}
		}

		public async Task<Student> RetrieveByIdAsync(Guid id)
		{
			try
			{
				var retrieveStudent = await this.studentRepository.SelectByIdAsync(id);

				if (retrieveStudent is null)
				{
					throw new StudentNotFoundException();
				}

				return retrieveStudent;
			}
			catch (StudentNotFoundException studentNotFoundException)
			{
				this.logger.LogError(studentNotFoundException);

				throw new StudentDependencyException(studentNotFoundException);
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedStudentStorageException(sqlException);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedStudentServiceException(exception);
			}
		}
	}
}
