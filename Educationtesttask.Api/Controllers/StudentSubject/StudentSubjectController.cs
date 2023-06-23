using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.ViewModels.StudentSubjects;
using Educationtesttask.Domain.Exceptions.StudentSubjects;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace Educationtesttask.Api.Controllers.StudentSubject
{
	[ApiController]
	[Route("/api/[controller]")]
	public class StudentSubjectController : RESTFulController
	{
		private readonly IStudentSubjectService studentSubjectService;

		public StudentSubjectController(IStudentSubjectService studentSubjectService) =>
			this.studentSubjectService = studentSubjectService;

		[HttpPost]
		public async Task<ActionResult> PostStudentSubject(StudentSubjectViewModel studentSubjectViewModel)
		{
			try
			{
				var result = await this.studentSubjectService.AddAsync(studentSubjectViewModel);

				return Created(result);
			}
			catch (StudentSubjectValidationException studentSubjectValidationException)
			{
				return BadRequest(studentSubjectValidationException);
			}
			catch (FailedStudentSubjectStorageException  failedStudentSubjectStorageException)
			{
				return InternalServerError(failedStudentSubjectStorageException);
			}
			catch (FailedStudentSubjectServiceException failedStudentSubjectServiceException)
			{
				return InternalServerError(failedStudentSubjectServiceException);
			}
		}

		[HttpGet]
		public ActionResult GetAllStudentSubjects()
		{
			try
			{
				var result = this.studentSubjectService.RetrieveAll(filter: null);

				return Ok(result);
			}
			catch (FailedStudentSubjectStorageException failedStudentSubjectStorageException)
			{
				return InternalServerError(failedStudentSubjectStorageException);
			}
			catch (FailedStudentSubjectServiceException failedStudentSubjectServiceException)
			{
				return InternalServerError(failedStudentSubjectServiceException);
			}
		}

		[HttpGet("{studentId}/{subjectId}")]
		public async Task<ActionResult> GetStudentSubject(Guid studentId, Guid subjectId)
		{
			try
			{
				var result = await this.studentSubjectService.RetrieveByIdAsync(studentId, subjectId);

				return Ok(result);
			}
			catch (StudentSubjectDependencyException exception)
				when (exception.InnerException is StudentSubjectNotFoundException)
			{
				return NotFound(exception.InnerException);
			}
			catch (FailedStudentSubjectStorageException failedStudentSubjectStorageException)
			{
				return InternalServerError(failedStudentSubjectStorageException);
			}
			catch (FailedStudentSubjectServiceException failedStudentSubjectServiceException)
			{
				return InternalServerError(failedStudentSubjectServiceException);
			}
		}

		[HttpPut]
		public async Task<ActionResult> PutStudentSubject(StudentSubjectViewModel viewModel)
		{
			try
			{
				var result = await this.studentSubjectService.ModifyAsync(viewModel);

				return NoContent();
			}
			catch (StudentSubjectValidationException exception)
			{
				return BadRequest(exception.InnerException);
			}
			catch (StudentSubjectDependencyException exception)
			    when (exception.InnerException is StudentSubjectNotFoundException)
			{
				return NotFound(exception.InnerException);
			}
			catch (FailedStudentSubjectStorageException failedStudentSubjectStorageException)
			{
				return InternalServerError(failedStudentSubjectStorageException);
			}
			catch (FailedStudentSubjectServiceException failedStudentSubjectServiceException)
			{
				return InternalServerError(failedStudentSubjectServiceException);
			}
		}

		[HttpDelete("{studentId}/{subjectId}")]
		public async Task<ActionResult> DeleteStudentSubject(Guid studentId, Guid subjectId)
		{
			try
			{
				var result = await this.studentSubjectService.DeleteAsync(studentId, subjectId);

				return NoContent();
			}
			catch (StudentSubjectDependencyException exception)
				when (exception.InnerException is StudentSubjectNotFoundException)
			{
				return NotFound(exception.InnerException);
			}
			catch (FailedStudentSubjectStorageException failedStudentSubjectStorageException)
			{
				return InternalServerError(failedStudentSubjectStorageException);
			}
			catch (FailedStudentSubjectServiceException failedStudentSubjectServiceException)
			{
				return InternalServerError(failedStudentSubjectServiceException);
			}
		}
	}
}
