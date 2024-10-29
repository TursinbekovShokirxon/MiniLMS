using MiniLMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLMS.Application.Services
{
    public interface ICourseRegistrationRepository
    {
        Task<IEnumerable<CourseRegistration>> GetAllRegistrationsAsync(); 
        Task<CourseRegistration> GetRegistrationByIdAsync(int id); 
        Task RegisterStudentAsync(CourseRegistration registration); 
        Task UnregisterStudentAsync(int id); 
    }
}
