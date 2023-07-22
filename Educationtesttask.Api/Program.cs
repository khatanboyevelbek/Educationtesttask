using System.Text;
using Educationtesttask.Application.Interfaces;
using Educationtesttask.Application.Logging;
using Educationtesttask.Application.Security;
using Educationtesttask.Application.Services;
using Educationtesttask.Application.Services.Account;
using Educationtesttask.Application.Services.StudentSubjects;
using Educationtesttask.Application.Services.Subjects;
using Educationtesttask.Application.Validations.Account;
using Educationtesttask.Application.Validations.Students;
using Educationtesttask.Application.Validations.StudentSubjects;
using Educationtesttask.Application.Validations.Subjects;
using Educationtesttask.Application.Validations.Teachers;
using Educationtesttask.Domain.DTOs.Students;
using Educationtesttask.Infrastructure.Data;
using Educationtesttask.Infrastructure.Interfaces;
using Educationtesttask.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;

namespace Educationtesttask.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers().AddNewtonsoftJson(options =>
			{
				options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			});

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddHttpContextAccessor();
			ConfigureSerilog(builder.Host);
			ConfigureCORS(builder.Services);
			RegisterDbContext(builder.Services, builder.Configuration);
			RegisterRepositories(builder.Services);
			RegisterUtilities(builder.Services);
			RegisterServices(builder.Services);
			RegisterAuthentication(builder.Services, builder.Configuration);
			ConfigureSwagger(builder.Services, builder.Configuration);


			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

            app.UseSerilogRequestLogging();
			app.UseCors("AllowAll");
			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();
            ValidatorOptions.Global.LanguageManager.Enabled = true;
            ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("en-US");
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
			services.AddTransient<TeacherCreateDtoValidation>();
			services.AddTransient<TeacherUpdateDtoValidation>();
			services.AddTransient<IValidator<StudentCreateDto>, StudentCreateDtoValidation>();
			services.AddTransient<IValidator<StudentUpdateDto>, StudentUpdateDtoValidation>();
			services.AddTransient<SubjectCreateDtoValidation>();
			services.AddTransient<SubjectUpdateDtoValidation>();
			services.AddTransient<StudentSubjectDtoValidation>();
			services.AddTransient<LoginModelValidation>();
		}
		private static void RegisterServices(IServiceCollection services)
		{
			services.AddTransient<ITeacherService, TeacherService>();
			services.AddTransient<IStudentService, StudentService>();
			services.AddTransient<ISubjectService, SubjectService>();
			services.AddTransient<IStudentSubjectService, StudentSubjectService>();
			services.AddTransient<IAuthManager, AuthManager>();
			services.AddTransient<ISecurityPassword, SecurityPassword>();
			services.AddTransient<IAuthService, AuthService>();
			services.AddTransient<IHttpContextCurrentUserProvider, HttpContextCurrentUserProvider>();
		}

		private static void RegisterAuthentication(IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = configuration["Jwt:ValidIssuer"],
						ValidAudience = configuration["Jwt:ValidAudience"],
						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
					};
				});
		}

		private static void ConfigureSwagger(IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1",
					new OpenApiInfo
					{ Title = "Educationtesttask.Api", Version = "v1" }
					);

				var securitySchema = new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "bearer",
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				};

				c.AddSecurityDefinition("Bearer", securitySchema);

				var securityRequirement = new OpenApiSecurityRequirement
				{
					{ securitySchema, new[] { "Bearer" } }
				};

				c.AddSecurityRequirement(securityRequirement);

			});
		}

		private static void ConfigureCORS(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", builder =>
				{
					builder.AllowAnyOrigin()
						   .AllowAnyMethod()
						   .AllowAnyHeader();
				});
			});
		}

		private static void ConfigureSerilog(ConfigureHostBuilder host)
		{
			host.UseSerilog((context, configuration) =>
				configuration.ReadFrom.Configuration(context.Configuration));
		}
	}
}