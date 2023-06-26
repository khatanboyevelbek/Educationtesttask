using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.ViewModels.Subjects;
using Educationtesttask.Domain.Enums;
using Educationtesttask.Domain.Exceptions.Subjects;
using Microsoft.AspNetCore.Authorization;
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

		[Authorize(Roles = nameof(Role.Teacher))]
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
			catch (SubjectDependencyException exception)
				 when (exception.InnerException is RestrictedAccessSubjectException)
			{
				return Forbidden(exception.InnerException);
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

		[Authorize(Roles = nameof(Role.Teacher) + "," + nameof(Role.Student))]
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

		[Authorize(Roles = nameof(Role.Teacher) + "," + nameof(Role.Student))]
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

		[Authorize(Roles = nameof(Role.Teacher))]
		[HttpPut]
		public async Task<ActionResult> PutSubject(SubjectUpdateViewModel viewModel)
		{
			try
			{
				var result = await this.subjectService.ModifyAsync(viewModel);

				return NoContent();
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
			catch (SubjectDependencyException exception)
				 when (exception.InnerException is RestrictedAccessSubjectException)
			{
				return Forbidden(exception.InnerException);
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

		[Authorize(Roles = nameof(Role.Teacher))]
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
			catch (SubjectDependencyException exception)
				 when (exception.InnerException is RestrictedAccessSubjectException)
			{
				return Forbidden(exception.InnerException);
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
