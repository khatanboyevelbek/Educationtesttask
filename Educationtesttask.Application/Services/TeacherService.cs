using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.Logging;
using Educationtesttask.Application.Validations;
using Educationtesttask.Application.ViewModels;
using Educationtesttask.Domain.Entities;
using Educationtesttask.Infrastructure.Interfaces;
using FluentValidation.Results;

namespace Educationtesttask.Application.Services
{
	public partial class TeacherService : ITeacherService
	{
		private readonly ISerilogLogger logger;
		private readonly ITeacherRepository teacherRepository;
		private readonly TeacherViewModelValidation validator;

		public TeacherService (ISerilogLogger logger,
			ITeacherRepository teacherRepository,
			TeacherViewModelValidation validator)
		{
			this.logger = logger;
			this.teacherRepository = teacherRepository;
			this.validator = validator;
		}

		public async Task<Teacher> AddAsync(TeacherViewModel viewModel)
		{
			try
			{
				ValidationResult validationResult = validator.Validate(viewModel);
				Validate(validationResult);

				var teacher = new Teacher()
				{
					Id = Guid.NewGuid(),
					FirstName = viewModel.FirstName,
					LastName = viewModel.LastName,
					PhoneNumber = viewModel.PhoneNumber,
					Email = viewModel.Email,
					BirthDate = viewModel.BirthDate,
					CreatedDate = DateTimeOffset.Now,
					UpdatedDate = DateTimeOffset.Now
				};

				Teacher addeddData = await this.teacherRepository.AddAsync(teacher);
				return addeddData;
            }
			catch (Exception ex)
			{
				throw;
			}
		}

		public Task<bool> DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<Teacher> ModifyAsync(TeacherViewModel viewModel)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Teacher> RetrieveAll(Expression<Func<Teacher, bool>> filter = null)
		{
			throw new NotImplementedException();
		}

		public Task<Teacher> RetrieveByIdAsync(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
