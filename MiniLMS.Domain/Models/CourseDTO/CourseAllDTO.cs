namespace MiniLMS.Domain.Models.CourseDTO
{
    public class CourseAllDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
    }

}
