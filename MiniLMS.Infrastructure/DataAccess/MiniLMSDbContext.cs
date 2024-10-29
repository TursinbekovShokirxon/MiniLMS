using Microsoft.EntityFrameworkCore;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Infrastructure.DataAccess;
public class MiniLMSDbContext : DbContext
{
    public MiniLMSDbContext()
    {

    }
        public MiniLMSDbContext(DbContextOptions<MiniLMSDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CourseRegistration> CourseRegistrations { get; set; }
        public DbSet<Teacher> Teachers { get; set; } // Добавляем таблицу преподавателей

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseRegistration>()
                .HasOne(cr => cr.Student)
                .WithMany()
                .HasForeignKey(cr => cr.StudentId);

            modelBuilder.Entity<CourseRegistration>()
                .HasOne(cr => cr.Course)
                .WithMany()
                .HasForeignKey(cr => cr.CourseId);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherId);
        }
    
}
