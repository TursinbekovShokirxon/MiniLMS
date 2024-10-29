namespace MiniLMS.Infrastructure.Services
{
    using MiniLMS.Application.Services;
    using MiniLMS.Domain.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync() => await _courseRepository.GetAllCoursesAsync();

        public async Task<Course> GetCourseByIdAsync(int id) => await _courseRepository.GetCourseByIdAsync(id);

        public async Task CreateCourseAsync(Course course) => await _courseRepository.CreateCourseAsync(course);

        public async Task UpdateCourseAsync(Course course) => await _courseRepository.UpdateCourseAsync(course);

        public async Task DeleteCourseAsync(int id) => await _courseRepository.DeleteCourseAsync(id);
    }
}