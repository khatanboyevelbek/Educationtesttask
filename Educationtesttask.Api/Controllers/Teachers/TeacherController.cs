﻿using Educationtesttask.Application.Interfaces;
using Educationtesttask.Domain.DTOs.Teachers;
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
		public async Task<ActionResult> PostTeacher(TeacherCreateDto teacherCreateViewModel)
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
		[HttpGet("filterbyage")]
		public ActionResult GetAllTeachers([FromQuery] int olderThan)
		{
			try
			{
				var result = this.teacherService.RetrieveAll(t => DateTime.Now.Year - t.BirthDate.Year > olderThan);

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
		[HttpGet("filterbyphonenumber/")]
		public ActionResult GetAllTeachersFilteredByMobileCompany([FromQuery] MobileOperators mobileOperators)
		{
			try
			{
				var result = this.teacherService.RetrieveAllFilteredByMobileOperators(mobileOperators);

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
		[HttpGet("{id}/subjects/")]
		public async Task<ActionResult> GetSubjectOfTeacherThatHasSomeStudnetAndMinValue(Guid id, [FromQuery] int hasNumberOfStudents, [FromQuery] int minimalGrade)
		{
			try
			{
				Subject subject = await this.teacherService.RetrieveSubjectOfTeacherThatHasSomeStudnetAndMinValue(id, hasNumberOfStudents, minimalGrade);

				return Ok(subject);
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

		[Authorize(Roles = nameof(Role.Student) + "," + nameof(Role.Teacher))]
		[HttpGet("filterbysubject/")]
		public ActionResult GetAllTeachersThatTeachSubjectsWithHighestGradeThanEnteredValue([FromQuery] int minGrade)
		{
			try
			{
				var result = this.teacherService.RetrieveAllTeachersThatTeachSubjectsWithHighestGradeThanEnteredValue(minGrade);

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

		[Authorize(Roles = nameof(Role.Teacher))]
		[HttpPut]
		public async Task<ActionResult> PutTeacher(TeacherUpdateDto viewModel)
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
