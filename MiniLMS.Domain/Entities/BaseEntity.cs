using MiniLMS.Domain.CustomeValidation;
using MiniLMS.Domain.States;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace MiniLMS.Domain.Entities;
public class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [NameValidation]
    public string FullName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public Gender Gender { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }
    [LoginValidation]
    public string Login { get; set; }
    [PasswordValidation]
    public string Password { get; set; }


}
