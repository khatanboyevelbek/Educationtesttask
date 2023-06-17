using Educationtesttask.Infrastructure.Data;
using Educationtesttask.Infrastructure.Interfaces;
using Educationtesttask.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Educationtesttask.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			RegisterDbContext(builder.Services, builder.Configuration);
			RegisterRepositories(builder.Services);

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

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
	}
}