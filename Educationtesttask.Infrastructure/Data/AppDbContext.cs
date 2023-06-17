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
		public virtual DbSet<Grade> Grades { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Relationship between Grade and Student
			modelBuilder.Entity<Grade>()
				.HasOne(g => g.Student)
				.WithMany(s => s.Grades)
				.HasForeignKey(g => g.StudentId)
				.OnDelete(DeleteBehavior.Restrict);

			// Relationship between Subject and Student
			modelBuilder.Entity<Subject>()
				.HasOne(s => s.Student)
				.WithMany(s => s.Subjects)
				.HasForeignKey(s => s.StudentId)
				.OnDelete(DeleteBehavior.Restrict);

			// Relationship between Subject and Teacher
			modelBuilder.Entity<Subject>()
				.HasOne(s => s.Teacher)
				.WithMany(t => t.Subjects)
				.HasForeignKey(s => s.TeacharId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
