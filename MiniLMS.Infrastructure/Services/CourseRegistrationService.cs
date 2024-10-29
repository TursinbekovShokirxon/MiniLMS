using MiniLMS.Application.Services;
using MiniLMS.Domain.Entities;

namespace MiniLMS.Infrastructure.Services
{
    public class CourseRegistrationService
    {
        private readonly ICourseRegistrationRepository _registrationRepository;

        public CourseRegistrationService(ICourseRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public Task<IEnumerable<CourseRegistration>> GetAllRegistrationsAsync() => _registrationRepository.GetAllRegistrationsAsync();

        public Task<CourseRegistration> GetRegistrationByIdAsync(int id) => _registrationRepository.GetRegistrationByIdAsync(id);

        public Task RegisterStudentAsync(CourseRegistration registration) => _registrationRepository.RegisterStudentAsync(registration);

        public Task UnregisterStudentAsync(int id) => _registrationRepository.UnregisterStudentAsync(id);
    }
}
