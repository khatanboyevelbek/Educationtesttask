using System.Linq.Expressions;
using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.Logging;
using Educationtesttask.Application.Validations;
using Educationtesttask.Application.ViewModels;
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
		private readonly TeacherViewModelValidation validator;

		public TeacherService (ISerilogLogger logger,
			ITeacherRepository teacherRepository,
			TeacherViewModelValidation validator)
		{
			this.logger = logger;
			this.teacherRepository = teacherRepository;
			this.validator = validator;
		}

		public async Task<Teacher> AddAsync(TeacherViewModel viewModel)
		{
			try
			{
				ValidationResult validationResult = validator.Validate(viewModel);

				if (viewModel is null)
				{
					throw new NullTeacherException();
				}

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
				this.logger.LogCritical(alreadyExistTeacherException);

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

		public Task<bool> DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<Teacher> ModifyAsync(TeacherViewModel viewModel)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Teacher> RetrieveAll(Expression<Func<Teacher, bool>> filter = null)
		{
			throw new NotImplementedException();
		}

		public Task<Teacher> RetrieveByIdAsync(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
