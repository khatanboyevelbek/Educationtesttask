using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.ViewModels.Teachers;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Domain.Exceptions.Teachers;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace Educationtesttask.Api.Controllers.Teachers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class TeacherController : RESTFulController
	{
		private readonly ITeacherService teacherService;

		public TeacherController(ITeacherService teacherService)
		{
			this.teacherService = teacherService;
		}

		[HttpPost]
		public async Task<ActionResult> PostTeacher(TeacherCreateViewModel teacherCreateViewModel)
		{
			try
			{
				var result = await this.teacherService.AddAsync(teacherCreateViewModel);

				return Created(result);
			}
			catch (TeacherValidationException exception)
			{
				return BadRequest(exception.InnerException);
			}
			catch (TeacherDependencyException exception) 
				when (exception.InnerException is AlreadyExistTeacherException) 
			{ 
				return Conflict(exception.InnerException);
			}
			catch (FailedTeacherStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedTeacherServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}

		[HttpGet]
		public ActionResult GetAllTeachers()
		{
			try
			{
				var result = this.teacherService.RetrieveAll(filter: null);

                return Ok(result);
			}
			catch (FailedTeacherStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedTeacherServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> GetTeacherById(Guid id)
		{
			try
			{
				var result = await this.teacherService.RetrieveByIdAsync(id);

				return Ok(result);
			}
			catch (TeacherDependencyException exception) 
				when (exception.InnerException is TeacherNotFoundException)
			{
				return NotFound(exception.InnerException);
			}
			catch (FailedTeacherStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedTeacherServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}

		[HttpPut]
		public async Task<ActionResult> PutTeacher(TeacherUpdateViewModel viewModel)
		{
			try
			{
				var result = await this.teacherService.ModifyAsync(viewModel);
				return Ok(result);
			}
			catch (TeacherValidationException exception)
			{
				return BadRequest(exception.InnerException);
			}
			catch (TeacherDependencyException exception)
				when (exception.InnerException is TeacherNotFoundException)
			{
				return NotFound(exception.InnerException);
			}
			catch (FailedTeacherStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedTeacherServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteTeacher(Guid id)
		{
			try
			{
				var result = await this.teacherService.DeleteAsync(id);

				return NoContent();
			}
			catch (TeacherDependencyException exception)
				when (exception.InnerException is TeacherNotFoundException)
			{
				return NotFound(exception.InnerException);
			}
			catch (FailedTeacherStorageException exception)
			{
				return InternalServerError(exception.InnerException);
			}
			catch (FailedTeacherServiceException exception)
			{
				return InternalServerError(exception.InnerException);
			}
		}
	}
}
