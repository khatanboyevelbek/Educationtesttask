using Educationtesttask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Educationtesttask.Infrastructure.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) 
			: base(options)
		{
			
		}

		public virtual DbSet<Teacher> Teachers { get; set; }
		public virtual DbSet<Student> Students { get; set; }
		public virtual DbSet<Subject> Subjects { get; set; }
		public virtual DbSet<StudentSubject> StudentSubjects { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<StudentSubject>()
			.HasKey(ss => new { ss.StudentId, ss.SubjectId });

			modelBuilder.Entity<StudentSubject>()
				.HasOne(ss => ss.Student)
				.WithMany(s => s.StudentSubjects)
				.HasForeignKey(ss => ss.StudentId);

			modelBuilder.Entity<StudentSubject>()
				.HasOne(ss => ss.Subject)
				.WithMany(s => s.StudentSubjects)
				.HasForeignKey(ss => ss.SubjectId);

			modelBuilder.Entity<Subject>()
				.HasOne(s => s.Teacher)
				.WithMany(t => t.Subjects)
				.HasForeignKey(s => s.TeacherId);
		}
	}
}
