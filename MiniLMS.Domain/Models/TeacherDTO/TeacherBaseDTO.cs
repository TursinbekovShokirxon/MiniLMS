using MiniLMS.Domain.States;

namespace MiniLMS.Domain.Models.TeacherDTO;

public class TeacherBaseDTO
{

    public string? FullName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public double? Salary { get; set; }
}
