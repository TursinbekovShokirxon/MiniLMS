using MiniLMS.Domain.States;

namespace MiniLMS.Domain.Entities;
public class Teacher : BaseEntity
{
    public IList<Course>? Courses { get; set; }
    public double? Salary { get; set; }
    public TeacherTypeState TeacherTypeState { get; set; }
}
