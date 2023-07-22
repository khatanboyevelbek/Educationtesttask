using Educationtesttask.Application.Interfaces;
using Educationtesttask.Domain.DTOs.Students;
using Educationtesttask.Domain.Enums;
using Educationtesttask.Domain.Exceptions.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace Educationtesttask.Api.Controllers.Students
{
	[ApiController]
	[Route("/api/[controller]")]
	public class StudentController : RESTFulController
	{
		private readonly IStudentService studentService;

		public StudentController(IStudentService studentService) =>
			this.studentService = studentService;

		[AllowAnonymous]
		[HttpPost]
		public async Task<ActionResult> PostStudent(StudentCreateDto viewModel)
		{
			try
			{
				var result = await this.studentService.AddAsync(viewModel);

				return Created(result);
			}
			catch (StudentValidationException exception)
			{
				return BadRequest(exception.InnerException);
			}
			catch (StudentDependencyException exception)
			     when (exception.InnerException is AlreadyExistStudentException)
			{
				return Conflict(exception.InnerException);
			}
			catch (FailedStudentStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedStudentServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}

		[Authorize(Roles = nameof(Role.Student) + "," + nameof(Role.Teacher))]
		[HttpGet]
		public ActionResult GetAllStudents()
		{
			try
			{
				var result = this.studentService.RetrieveAll(filter: null);

				return Ok(result);
			}
			catch (FailedStudentStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedStudentServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}

		[Authorize(Roles = nameof(Role.Student) + "," + nameof(Role.Teacher))]
		[HttpGet("filterbyage/")]
		public ActionResult GetAllStudentsFileteredBySpecificAge([FromQuery] int youngerThan)
		{
			try
			{
				var result = this.studentService.RetrieveAll(s => DateTime.Now.Year - s.BirthDate.Year < youngerThan);

				return Ok(result);
			}
			catch (FailedStudentStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedStudentServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}

		[Authorize(Roles = nameof(Role.Student) + "," + nameof(Role.Teacher))]
		[HttpGet("filterbybirthdate/")]
		public ActionResult GetAllStudentsFilteredBySpecificDates([FromQuery] int startMonth, [FromQuery] int startDay,
			[FromQuery] int endMonth, [FromQuery] int endDay)
		{
			try
			{
				var result = this.studentService.RetrieveAllFileteredByBirthDate(startMonth, startDay, endMonth, endDay);

				return Ok(result);
			}
			catch (FailedStudentStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedStudentServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}

		[Authorize(Roles = nameof(Role.Student) + "," + nameof(Role.Teacher))]
		[HttpGet("filterbyphonenumber/")]
		public ActionResult GetAllStudents([FromQuery] MobileOperators mobileOperators)
		{
			try
			{
				var result = this.studentService.RetrieveAllFilteredByMobileOperators(mobileOperators);

				return Ok(result);
			}
			catch (FailedStudentStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedStudentServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}

		[Authorize(Roles = nameof(Role.Student) + "," + nameof(Role.Teacher))]
		[HttpGet("filterbyphrase/")]
		public ActionResult GetAllStudentsThatContainEnteredPhrase([FromQuery] string phrase)
		{
			try
			{
				var result = this.studentService.RetrieveAllThatContainEnteredPhrase(phrase);

				return Ok(result);
			}
			catch (FailedStudentStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedStudentServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}

		[Authorize(Roles = nameof(Role.Student) + "," + nameof(Role.Teacher))]
		[HttpGet("{id}")]
		public async Task<ActionResult> GetStudent (Guid id)
		{
			try
			{
				var result = await this.studentService.RetrieveByIdAsync(id);

				return Ok(result);
			}
			catch (StudentDependencyException exception)
				 when (exception.InnerException is StudentNotFoundException)
			{
				return NotFound(exception.InnerException);
			}
			catch (FailedStudentStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedStudentServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}

		[Authorize(Roles = nameof(Role.Student) + "," + nameof(Role.Teacher))]
		[HttpGet("{id}/studentsubjects/")]
		public async Task<ActionResult> GetStudentSubjectsOfStudentWithHighGrade(Guid id, [FromQuery] bool highGrade)
		{
			try
			{
				var result = await this.studentService.RetrieveStudentSubjectsOfStudentWithHighGrade(id, highGrade);

				return Ok(result);
			}
			catch (StudentDependencyException exception)
				 when (exception.InnerException is StudentNotFoundException)
			{
				return NotFound(exception.InnerException);
			}
			catch (FailedStudentStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedStudentServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}

		[Authorize(Roles = nameof(Role.Student))]
		[HttpPut]
		public async Task<ActionResult> PutStudent(StudentUpdateDto viewModel)
		{
			try
			{
				var result = await this.studentService.ModifyAsync(viewModel);

				return NoContent();
			}
			catch (StudentValidationException exception)
			{
				return BadRequest(exception.InnerException);
			}
			catch (StudentDependencyException exception)
				when (exception.InnerException is StudentNotFoundException)
			{
				return NotFound(exception.InnerException);
			}
			catch (StudentDependencyException exception)
				when (exception.InnerException is RestrictAccessStudentException)
			{
				return Forbidden(exception.InnerException);
			}
			catch (FailedStudentStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedStudentServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}

		[Authorize(Roles = nameof(Role.Student))]
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteStudent(Guid id)
		{
			try
			{
				var result = await this.studentService.DeleteAsync(id);

				return NoContent();
			}
			catch (StudentDependencyException exception)
				when (exception.InnerException is StudentNotFoundException)
			{
				return NotFound(exception.InnerException);
			}
			catch (StudentDependencyException exception)
				when (exception.InnerException is RestrictAccessStudentException)
			{
				return Forbidden(exception.InnerException);
			}
			catch (FailedStudentStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedStudentServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}
	}
}
