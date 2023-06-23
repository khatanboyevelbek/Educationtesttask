using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.ViewModels.Subjects;
using Educationtesttask.Domain.Exceptions.Subjects;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace Educationtesttask.Api.Controllers.Subjects
{
	[ApiController]
	[Route("/api/[controller]")]
	public class SubjectController : RESTFulController
	{
		private readonly ISubjectService subjectService;
		public SubjectController(ISubjectService subjectService) =>
			this.subjectService = subjectService;

		[HttpPost]
		public async Task<ActionResult> PostSubject(SubjectCreateViewModel viewModel)
		{
			try
			{
				var result = await this.subjectService.AddAsync(viewModel);

				return Created(result);
			}
			catch (SubjectValidationException exception)
			{
				return BadRequest(exception.InnerException);
			}
			catch (SubjectDependencyException exception)
			     when (exception.InnerException is AlreadyExistSubjectException)
			{
				return Conflict(exception.InnerException);
			}
			catch (FailedSubjectStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedSubjectServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}

		[HttpGet]
		public ActionResult GetAllSubjects()
		{
			try
			{
				var result = this.subjectService.RetrieveAll(filter: null);

				return Ok(result);
			}
			catch (FailedSubjectStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedSubjectServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> GetSubject(Guid id)
		{
			try
			{
				var result = await this.subjectService.RetrieveByIdAsync(id);

				return Ok(result);
			}
			catch (SubjectDependencyException exception)
				when (exception.InnerException is SubjectNotFoundException)
			{
				return NotFound(exception.InnerException);
			}
			catch (FailedSubjectStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedSubjectServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}

		[HttpPut]
		public async Task<ActionResult> PutSubject(SubjectUpdateViewModel viewModel)
		{
			try
			{
				var result = await this.subjectService.ModifyAsync(viewModel);

				return Ok(result);
			}
			catch (SubjectValidationException exception)
			{
				return BadRequest(exception.InnerException);
			}
			catch (SubjectDependencyException exception)
				when (exception.InnerException is SubjectNotFoundException)
			{
				return NotFound(exception.InnerException);
			}
			catch (FailedSubjectStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedSubjectServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteSubject(Guid id)
		{
			try
			{
				var result = await this.subjectService.DeleteAsync(id);

				return NoContent();
			}
			catch (SubjectDependencyException exception)
				when (exception.InnerException is SubjectNotFoundException)
			{
				return NotFound(exception.InnerException);
			}
			catch (FailedSubjectStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedSubjectServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}
	}
}
