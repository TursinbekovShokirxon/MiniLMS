using MiniLMS.Application.Services;
using MiniLMS.Domain.Entities;
using MiniLMS.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLMS.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        public MiniLMSDbContext _db;
        public CourseRepository( MiniLMSDbContext db)
        {
            _db=db;
        }
        public Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return Task.FromResult(_db.Courses.AsEnumerable());
        }

        public Task<Course> GetCourseByIdAsync(int id)
        {
            var course = _db.Courses.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(course);
        }

        public Task CreateCourseAsync(Course course)
        {
            _db.Courses.Add(course);
            _db.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task UpdateCourseAsync(Course course)
        {
            var existingCourse = _db.Courses.FirstOrDefault(c => c.Id == course.Id);
            if (existingCourse != null)
            {
                existingCourse.Title = course.Title;
                existingCourse.Description = course.Description;
                existingCourse.TeacherId = course.TeacherId;
            }
            await _db.SaveChangesAsync();
            return;
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = _db.Courses.FirstOrDefault(c => c.Id == id);
            if (course != null)
            {
                _db.Courses.Remove(course);
            }
            await _db.SaveChangesAsync();
            return;
        }
    }
}

