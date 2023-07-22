using System.Linq.Expressions;
using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.Logging;
using Educationtesttask.Application.Validations.Students;
using Educationtesttask.Domain.DTOs.Students;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Domain.Entities.Account;
using Educationtesttask.Domain.Enums;
using Educationtesttask.Domain.Exceptions.Students;
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
		private readonly ISecurityPassword securityPassword;
		private readonly IHttpContextCurrentUserProvider httpContextCurrentUserProvider;

		public StudentService(IStudentRepository studentRepository, ISerilogLogger logger,
			StudentCreateViewModelValidation validatorCreate,
			StudentUpdateViewModelValidation validatorUpdate, 
			ISecurityPassword securityPassword,
			IHttpContextCurrentUserProvider httpContextCurrentUserProvider)
		{
			this.studentRepository = studentRepository;
			this.logger = logger;
			this.validatorCreate = validatorCreate;
			this.validatorUpdate = validatorUpdate;
			this.securityPassword = securityPassword;
			this.httpContextCurrentUserProvider = httpContextCurrentUserProvider;
		}

		public async Task<Student> AddAsync(StudentCreateDto viewModel)
		{
			try
			{
				if (viewModel is null)
				{
					throw new NullStudentException();
				}

				//ValidationResult validationResult = validatorCreate.Validate(viewModel);
				//Validate(validationResult);

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
					Password = this.securityPassword.Encrypt(viewModel.Password),
					Role = Role.Student,
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
				UserClaims currentUser = this.httpContextCurrentUserProvider.GetCurrentUser();

				if (currentUser.Role != Role.Student)
				{
					throw new RestrictAccessStudentException();
				}

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
			catch (RestrictAccessStudentException studentNotFoundException)
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

		public async Task<Student> ModifyAsync(StudentUpdateDto viewModel)
		{
			try
			{
				UserClaims currentUser = this.httpContextCurrentUserProvider.GetCurrentUser();

				if (currentUser.Role != Role.Student)
				{
					throw new RestrictAccessStudentException();
				}

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
				retrieveExistingStudent.Password = this.securityPassword.Encrypt(viewModel.Password);
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
			catch (RestrictAccessStudentException studentNotFoundException)
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

		public IQueryable<Student> RetrieveAll(Expression<Func<Student, bool>> filter)
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

		public IQueryable<Student> RetrieveAllThatContainEnteredPhrase(string phrase)
		{
			try
			{
				return this.studentRepository.SelectAllAsync(s => s.FirstName.Contains(phrase) || s.LastName.Contains(phrase));
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

		public IQueryable<Student> RetrieveAllFileteredByBirthDate(int startMonth, int startDay, int endMonth, int endDay)
		{
			try
			{
				Expression<Func<Student, bool>> expression = s =>
					(s.BirthDate.Month > startMonth || (s.BirthDate.Month == startMonth && s.BirthDate.Day >= startDay)) &&
					(s.BirthDate.Month < endMonth || (s.BirthDate.Month == endMonth && s.BirthDate.Day <= endDay));

				return this.studentRepository.SelectAllAsync(expression);
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

		public IQueryable<Student> RetrieveAllFilteredByMobileOperators(MobileOperators mobileOperators)
		{
			try
			{

				return mobileOperators switch
				{
					MobileOperators.Beeline => 
						this.studentRepository.SelectAllAsync(s => s.PhoneNumber.StartsWith("+99890") || s.PhoneNumber.StartsWith("+99891")),

					MobileOperators.Ucell =>
						this.studentRepository.SelectAllAsync(s => s.PhoneNumber.StartsWith("+99893") || s.PhoneNumber.StartsWith("+99894")),

					MobileOperators.Uzmobile =>
						this.studentRepository.SelectAllAsync(s => s.PhoneNumber.StartsWith("+99895") || s.PhoneNumber.StartsWith("+99899")),

					MobileOperators.Umc =>
						this.studentRepository.SelectAllAsync(s => s.PhoneNumber.StartsWith("+99897")),

					MobileOperators.Humans =>
						this.studentRepository.SelectAllAsync(s => s.PhoneNumber.StartsWith("+99833")),

					_ => this.studentRepository.SelectAllAsync()
				};
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

		public async Task<Subject> RetrieveStudentSubjectsOfStudentWithHighGrade(Guid id, bool highGrade)
		{
			try
			{
				var retrieveStudent = await this.studentRepository.SelectByIdAsync(id);

				if (retrieveStudent is null)
				{
					throw new StudentNotFoundException();
				}

				int maxGrade = retrieveStudent.StudentSubjects.Max(s => s.Grade);
				Subject highGradedSubject = retrieveStudent.StudentSubjects.FirstOrDefault(s => s.Grade == maxGrade)?.Subject;

				return highGradedSubject;
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
