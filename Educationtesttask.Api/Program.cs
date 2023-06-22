using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.Logging;
using Educationtesttask.Application.Services;
using Educationtesttask.Application.Services.Subjects;
using Educationtesttask.Application.Validations;
using Educationtesttask.Application.Validations.Students;
using Educationtesttask.Application.Validations.Subjects;
using Educationtesttask.Application.Validations.Teachers;
using Educationtesttask.Application.ViewModels.Subjects;
using Educationtesttask.Infrastructure.Data;
using Educationtesttask.Infrastructure.Interfaces;
using Educationtesttask.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Educationtesttask.Api
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Host.UseSerilog((context, configuration) => 
				configuration.ReadFrom.Configuration(context.Configuration));

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			RegisterDbContext(builder.Services, builder.Configuration);
			RegisterRepositories(builder.Services);
			RegisterUtilities(builder.Services);
			RegisterServices(builder.Services);
			

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseSerilogRequestLogging();
			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.MapControllers();

			app.Run();
		}

		private static void RegisterDbContext(IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
			});
		}
		private static void RegisterRepositories(IServiceCollection services)
		{
			services.AddTransient<ITeacherRepository, TeacherRepository>();
			services.AddTransient<IStudentRepository, StudentRepository>();
			services.AddTransient<IStudentSubjectRepository, StudentSubjectRepository>();
			services.AddTransient<ISubjectRepository, SubjectRepository>();
		}
		private static void RegisterUtilities(IServiceCollection services)
		{
			services.AddScoped<ISerilogLogger, SerilogLogger>();
			services.AddTransient<TeacherCreateViewModelValidation>();
			services.AddTransient<TeacherUpdateViewModelValidation>();
			services.AddTransient<StudentCreateViewModelValidation>();
			services.AddTransient<StudentUpdateViewModelValidation>();
			services.AddTransient<SubjectCreateViewModelValidation>();
			services.AddTransient<SubjectUpdateViewModelValidation>();
		}
		private static void RegisterServices(IServiceCollection services)
		{
			services.AddTransient<ITeacherService, TeacherService>();
			services.AddTransient<IStudentService, StudentService>();
			services.AddTransient<ISubjectService, SubjectService>();
		}
	}
}