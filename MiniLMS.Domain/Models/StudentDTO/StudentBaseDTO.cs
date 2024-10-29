using MiniLMS.Domain.Entities;
using MiniLMS.Domain.States;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MiniLMS.Domain.Models.StudentDTO;
public class StudentBaseDTO:IValidatableObject
{
    public string? FullName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string Major { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        int dateNow = DateTime.Now.Year;
        if(dateNow-DateTime.Parse(BirthDate.ToString()).Year < 18)
        {
            yield return new ValidationResult("Not Allow");
        }
    }
}
