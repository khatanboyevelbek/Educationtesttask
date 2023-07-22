using System.Linq.Expressions;
using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.Logging;
using Educationtesttask.Application.Validations.Teachers;
using Educationtesttask.Domain.DTOs.Teachers;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Domain.Entities.Account;
using Educationtesttask.Domain.Enums;
using Educationtesttask.Domain.Exceptions.Subjects;
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
		private readonly ISecurityPassword securityPassword;
		private readonly IHttpContextCurrentUserProvider httpContextCurrentUserProvider;


		public TeacherService (ISerilogLogger logger,
			ITeacherRepository teacherRepository,
			TeacherCreateViewModelValidation validatorCreate,
			TeacherUpdateViewModelValidation validatorUpdate,
			ISecurityPassword securityPassword,
			IHttpContextCurrentUserProvider httpContextCurrentUserProvider)
		{
			this.logger = logger;
			this.teacherRepository = teacherRepository;
			this.validatorCreate = validatorCreate;
			this.validatorUpdate = validatorUpdate;
			this.securityPassword = securityPassword;
			this.httpContextCurrentUserProvider = httpContextCurrentUserProvider;
		}

		public async Task<Teacher> AddAsync(TeacherCreateDto viewModel)
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
					Password = this.securityPassword.Encrypt(viewModel.Password),
					Role = Role.Teacher,
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
				UserClaims currentUser = this.httpContextCurrentUserProvider.GetCurrentUser();

				if (currentUser.Role != Role.Teacher)
				{
					throw new RestrictAccessTeacherException();
				}

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
			catch (RestrictAccessTeacherException restrictAccessTeacherException)
			{
				this.logger.LogError(restrictAccessTeacherException);

				throw new TeacherDependencyException(restrictAccessTeacherException);
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

		public async Task<Teacher> ModifyAsync(TeacherUpdateDto viewModel)
		{
			try
			{
				UserClaims currentUser = this.httpContextCurrentUserProvider.GetCurrentUser();

				if (currentUser.Role != Role.Teacher)
				{
					throw new RestrictAccessTeacherException();
				}

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
				retrieveExistingTeacher.Password = this.securityPassword.Encrypt(viewModel.Password);
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
			catch (RestrictAccessTeacherException restrictAccessTeacherException)
			{
				this.logger.LogError(restrictAccessTeacherException);

				throw new TeacherDependencyException(restrictAccessTeacherException);
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

		public IQueryable<Teacher> RetrieveAllFilteredByMobileOperators(MobileOperators mobileOperators)
		{
			try
			{
				return mobileOperators switch
				{
					MobileOperators.Beeline =>
						this.teacherRepository.SelectAllAsync(s => s.PhoneNumber.StartsWith("+99890") || s.PhoneNumber.StartsWith("+99891")),

					MobileOperators.Ucell =>
						this.teacherRepository.SelectAllAsync(s => s.PhoneNumber.StartsWith("+99893") || s.PhoneNumber.StartsWith("+99894")),

					MobileOperators.Uzmobile =>
						this.teacherRepository.SelectAllAsync(s => s.PhoneNumber.StartsWith("+99895") || s.PhoneNumber.StartsWith("+99899")),

					MobileOperators.Umc =>
						this.teacherRepository.SelectAllAsync(s => s.PhoneNumber.StartsWith("+99897")),

					MobileOperators.Humans =>
						this.teacherRepository.SelectAllAsync(s => s.PhoneNumber.StartsWith("+99833")),

					_ => this.teacherRepository.SelectAllAsync()
				};
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

		public IQueryable<Teacher> RetrieveAllTeachersThatTeachSubjectsWithHighestGradeThanEnteredValue(int minGrade)
		{
			try
			{
				IQueryable<Teacher> retrieveTeachers = this.teacherRepository.SelectAllAsync().Where(t => 
					t.Subjects.Any(s => s.StudentSubjects.Any(ss => ss.Grade >= minGrade)));

				return retrieveTeachers;
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

		public async Task<Subject> RetrieveSubjectOfTeacherThatHasSomeStudnetAndMinValue(Guid id, int hasNumberOfStudents, int minimalGrade)
		{
			try
			{
				var retrievedTeacher = await this.teacherRepository.SelectByIdAsync(id);

				if (retrievedTeacher is null)
				{
					throw new TeacherNotFoundException();
				}

				Subject subject = retrievedTeacher.Subjects.FirstOrDefault(s =>
					s.StudentSubjects.Count(ss => ss.Grade >= minimalGrade) >= hasNumberOfStudents);

				return subject;

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
