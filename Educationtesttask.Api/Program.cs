using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.Logging;
using Educationtesttask.Application.Services;
using Educationtesttask.Application.Validations;
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
			services.AddTransient<IGradeRepository, GradeRepository>();
			services.AddTransient<ISubjectRepository, SubjectRepository>();
		}
		private static void RegisterUtilities(IServiceCollection services)
		{
			services.AddScoped<ISerilogLogger, SerilogLogger>();
			services.AddTransient<TeacherViewModelValidation>();
		}
		private static void RegisterServices(IServiceCollection services)
		{
			services.AddTransient<ITeacherService, TeacherService>();
		}
	}
}