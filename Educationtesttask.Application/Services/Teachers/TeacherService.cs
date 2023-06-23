using System.Linq.Expressions;
using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.Logging;
using Educationtesttask.Application.Validations;
using Educationtesttask.Application.Validations.Teachers;
using Educationtesttask.Application.ViewModels.Teachers;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Domain.Exceptions.Teachers;
using Educationtesttask.Infrastructure.Interfaces;
using FluentValidation.Results;
using Microsoft.Data.SqlClient;

namespace Educationtesttask.Application.Services
{
    public partial class TeacherService : ITeacherService
	{
		private readonly ISerilogLogger logger;
		private readonly ITeacherRepository teacherRepository;
		private readonly TeacherCreateViewModelValidation validatorCreate;
		private readonly TeacherUpdateViewModelValidation validatorUpdate;


		public TeacherService (ISerilogLogger logger,
			ITeacherRepository teacherRepository,
			TeacherCreateViewModelValidation validatorCreate,
			TeacherUpdateViewModelValidation validatorUpdate)
		{
			this.logger = logger;
			this.teacherRepository = teacherRepository;
			this.validatorCreate = validatorCreate;
			this.validatorUpdate = validatorUpdate;
		}

		public async Task<Teacher> AddAsync(TeacherCreateViewModel viewModel)
		{
			try
			{
				if (viewModel is null)
				{
					throw new NullTeacherException();
				}

				ValidationResult validationResult = validatorCreate.Validate(viewModel);
				Validate(validationResult);
				bool existingTeacher = this.teacherRepository.SelectAllAsync().Any(t => t.Email == viewModel.Email);

				if(existingTeacher)
				{
					throw new AlreadyExistTeacherException();
				}

				var teacher = new Teacher()
				{
					Id = Guid.NewGuid(),
					FirstName = viewModel.FirstName,
					LastName = viewModel.LastName,
					PhoneNumber = viewModel.PhoneNumber,
					Email = viewModel.Email,
					Password = viewModel.Password,
					BirthDate = viewModel.BirthDate,
					CreatedDate = DateTimeOffset.Now,
					UpdatedDate = DateTimeOffset.Now
				};

				Teacher addeddData = await this.teacherRepository.AddAsync(teacher);
				return addeddData;
            }
			catch (InvalidTeacherException invalidTeacherException)
			{
				this.logger.LogError(invalidTeacherException);

				throw new TeacherValidationException(invalidTeacherException);
			}
			catch(NullTeacherException nullTeacherException)
			{
				this.logger.LogError(nullTeacherException);

				throw new TeacherValidationException(nullTeacherException);
			}
			catch(AlreadyExistTeacherException alreadyExistTeacherException)
			{
				this.logger.LogError(alreadyExistTeacherException);

				throw new TeacherDependencyException(alreadyExistTeacherException);
			}
			catch(SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedTeacherStorageException(sqlException);
			}
			catch(Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedTeacherServiceException(exception);
			}
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			try
			{
				Teacher existingEntity = await this.teacherRepository.SelectByIdAsync(id);

				if(existingEntity is null)
				{
					throw new TeacherNotFoundException();
				}

				return await this.teacherRepository.DeleteAsync(existingEntity);
			}
			catch (TeacherNotFoundException teacherNotFoundException)
			{
				this.logger.LogError(teacherNotFoundException);

				throw new TeacherDependencyException(teacherNotFoundException);
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedTeacherStorageException(sqlException);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedTeacherServiceException(exception);
			}
		}

		public async Task<Teacher> ModifyAsync(TeacherUpdateViewModel viewModel)
		{
			try
			{
				ValidationResult validationResult = validatorUpdate.Validate(viewModel);
				Validate(validationResult);

				var retrieveExistingTeacher = await this.teacherRepository.SelectByIdAsync(viewModel.Id);

				if(retrieveExistingTeacher is null)
				{
					throw new TeacherNotFoundException();
				}

				retrieveExistingTeacher.FirstName = viewModel.FirstName;
				retrieveExistingTeacher.LastName = viewModel.LastName;
				retrieveExistingTeacher.PhoneNumber = viewModel.PhoneNumber;
				retrieveExistingTeacher.Email = viewModel.Email;
				retrieveExistingTeacher.Password = viewModel.Password;
				retrieveExistingTeacher.BirthDate = viewModel.BirthDate;
				retrieveExistingTeacher.UpdatedDate = DateTimeOffset.Now;

				var updatedTeacher = await this.teacherRepository.UpdateAsync(retrieveExistingTeacher);

				return updatedTeacher;
			}
			catch (InvalidTeacherException invalidTeacherException)
			{
				this.logger.LogError(invalidTeacherException);

				throw new TeacherValidationException(invalidTeacherException);
			}
			catch (NullTeacherException nullTeacherException)
			{
				this.logger.LogError(nullTeacherException);

				throw new TeacherValidationException(nullTeacherException);
			}
			catch (TeacherNotFoundException teacherNotFoundException)
			{
				this.logger.LogError(teacherNotFoundException);

				throw new TeacherDependencyException(teacherNotFoundException);
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedTeacherStorageException(sqlException);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedTeacherServiceException(exception);
			}
		}

		public IQueryable<Teacher> RetrieveAll(Expression<Func<Teacher, bool>> filter)
		{
			try
			{
				var result = this.teacherRepository.SelectAllAsync(filter);

				return result;
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedTeacherStorageException(sqlException);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedTeacherServiceException(exception);
			}
		}

		public async Task<Teacher> RetrieveByIdAsync(Guid id)
		{
			try
			{
				var retrieveTeacher = await this.teacherRepository.SelectByIdAsync(id);

				if (retrieveTeacher is null)
				{
					throw new TeacherNotFoundException();
				}

				return retrieveTeacher;
			}
			catch (TeacherNotFoundException teacherNotFoundException)
			{
				this.logger.LogError(teacherNotFoundException);

				throw new TeacherDependencyException(teacherNotFoundException);
			}
			catch (SqlException sqlException)
			{
				this.logger.LogCritical(sqlException);

				throw new FailedTeacherStorageException(sqlException);
			}
			catch (Exception exception)
			{
				this.logger.LogCritical(exception);

				throw new FailedTeacherServiceException(exception);
			}
		}
	}
}
