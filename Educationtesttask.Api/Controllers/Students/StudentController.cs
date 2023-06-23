using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.ViewModels.Students;
using Educationtesttask.Domain.Exceptions.Students;
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

		[HttpPost]
		public async Task<ActionResult> PostStudent(StudentCreateViewModel viewModel)
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

		[HttpPut]
		public async Task<ActionResult> PutStudent(StudentUpdateViewModel viewModel)
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
			catch (FailedStudentStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedStudentServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}

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
