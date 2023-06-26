using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.ViewModels.Teachers;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Domain.Enums;
using Educationtesttask.Domain.Exceptions.Teachers;
using Microsoft.AspNetCore.Authorization;
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

		[AllowAnonymous]
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

		[Authorize(Roles = nameof(Role.Student) + "," + nameof(Role.Teacher))]
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

		[Authorize(Roles = nameof(Role.Student) + "," + nameof(Role.Teacher))]
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

		[Authorize(Roles = nameof(Role.Teacher))]
		[HttpPut]
		public async Task<ActionResult> PutTeacher(TeacherUpdateViewModel viewModel)
		{
			try
			{
				var result = await this.teacherService.ModifyAsync(viewModel);

				return NoContent();
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
			catch (TeacherDependencyException exception)
				when (exception.InnerException is RestrictAccessTeacherException)
			{
				return Forbidden(exception.InnerException);
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

		[Authorize(Roles = nameof(Role.Teacher))]
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
			catch (TeacherDependencyException exception)
				when (exception.InnerException is RestrictAccessTeacherException)
			{
				return Forbidden(exception.InnerException);
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
