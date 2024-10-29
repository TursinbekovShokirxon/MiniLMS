using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiniLMS.Application.Services;
using MiniLMS.Domain.Entities;
using MiniLMS.Domain.Models.CourseDTO;
using MiniLMS.Infrastructure.Services;

namespace MiniLMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CourseService _courseService;
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;

        public CoursesController(CourseService courseService, IMapper mapper, ITeacherService teacherService)
        {
            _courseService = courseService;
            _teacherService = teacherService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();

            var coursesDto = courses.Select(x => new CourseAllDTO
            {
                Id = x.Id,
                Description = x.Description,
                Title = x.Title
            }).ToList();

            return Ok(coursesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CourseCreateDTO course)
        {
            Course newCourse = new()
            {
                Description = course.Description,
                Title = course.Title
            };
            await _courseService.CreateCourseAsync(newCourse);
            return CreatedAtAction(nameof(GetCourse), new { id = newCourse.Id }, course);
        }

        [HttpPut("RemoveTeacherToCourse")]
        public async Task<IActionResult> RemoveTeacherInCourse(int courseId,int teacherId )
        {
            var course = await _courseService.GetCourseByIdAsync(courseId);
            var teacher = await _teacherService.GetByIdAsync(teacherId);
            if (course == null) return BadRequest("This course hasn't in db");
            if (teacher == null) return BadRequest("This teacher hasn't in db");
            course.Teacher = null;
            await _courseService.UpdateCourseAsync(course);
            return Ok("Teacher successfully removed to course!");
        }

        [HttpPut("AddTeacherToCourse")]
        public async Task<IActionResult> AddTeacherToCourse(int courseId, int teacherId)
        {
            var course = await _courseService.GetCourseByIdAsync(courseId);
            var teacher = await _teacherService.GetByIdAsync(teacherId);
            if (course == null) return BadRequest("This course hasn't in db");
            if (teacher == null) return BadRequest("This teacher hasn't in db");
            course.Teacher = teacher;
            await _courseService.UpdateCourseAsync(course);
            return Ok("Teacher successfully added to course!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return NoContent();
        }
    }
}
