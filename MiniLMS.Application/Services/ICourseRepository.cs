using MiniLMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLMS.Application.Services
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync(); 
        Task<Course> GetCourseByIdAsync(int id); 
        Task CreateCourseAsync(Course course); 
        Task UpdateCourseAsync(Course course); 
        Task DeleteCourseAsync(int id); 
    }
}
