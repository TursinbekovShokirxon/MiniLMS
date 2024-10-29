using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiniLMS.Application.Services;
using MiniLMS.Domain.Entities;
using MiniLMS.Domain.Models.CourseRegistrationDTO;
using MiniLMS.Infrastructure.Services;

namespace MiniLMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly CourseRegistrationService _registrationService;
        private readonly IStudentService _studentService;
        private readonly ICourseRepository _courseService;
        private readonly IMapper _mapper;

        public RegistrationController(CourseRegistrationService registrationService,
            IMapper mapper,
            IStudentService studentService,
            ICourseRepository teacherService)
        {
            _registrationService = registrationService;
            _studentService = studentService;
            _courseService = teacherService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRegistrations()
        {
            var registrations = await _registrationService.GetAllRegistrationsAsync();
            var registrationDtos = _mapper.Map<List<CourseRegistrationAllDTO>>(registrations);
            return Ok(registrationDtos);
        }

        [HttpGet("GetRegistrationById")]
        public async Task<IActionResult> GetRegistrationById(int id)
        {
            var registration = await _registrationService.GetRegistrationByIdAsync(id);
            if (registration == null) return NotFound();
            var registrationDto = _mapper.Map<CourseRegistrationAllDTO>(registration);
            return Ok(registrationDto);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStudent([FromBody]CourseRegistrationAllDTO registrationDto)
        {

            var student =await _studentService.GetByIdAsync(registrationDto.StudentId);
            var course =await _courseService.GetCourseByIdAsync(registrationDto.CourseId);
            if(student==null) return NotFound("This student not found");
            if(course == null) return NotFound("This course not found");
            var registration = _mapper.Map<CourseRegistration>(registrationDto);
            registration.Course = course;
            registration.Student= student;
            await _registrationService.RegisterStudentAsync(registration);
            return Ok("Successfully registred");
        }

        [HttpDelete("UnRegister")]
        public async Task<IActionResult> UnregisterStudent(int studentId,int courseId)
        {
            var student = await _studentService.GetByIdAsync(studentId);
            var course = await _courseService.GetCourseByIdAsync(courseId);
            
          
            if (student == null) return NotFound("This student not found");
            if (course == null) return NotFound("This course not found");
            var studentInDb = course.Students;

            if (studentInDb == null)
                return NotFound("Student not exist's"); 
            

            var registration = _registrationService.GetAllRegistrationsAsync().Result.FirstOrDefault(x=>x.CourseId==courseId && x.StudentId==studentId);
            await _registrationService.UnregisterStudentAsync(registration.Id);
            return Ok("Successsfuly UnregisterStudent!");
        }
    }
}
