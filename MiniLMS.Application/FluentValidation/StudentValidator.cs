using FluentValidation;
using MiniLMS.Application.Services;
using MiniLMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiniLMS.Application.FluentValidation
{
    public class StudentValidator:AbstractValidator<Student>
    {
        public ITeacherService _teacherServices;
        public StudentValidator(ITeacherService teacherService)
        {
           /**= _teacherServices = teacherService;
            RuleFor(x => x.FullName).Must(x => (Char.IsUpper(x[0])));
            RuleFor(x => x.PhoneNumber).NotEmpty()
           .NotNull().WithMessage("Phone Number is required.")
           .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
           .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
           .Matches(new Regex("(?:\\+[9]{2}[8][0-9]{2}\\ [0-9]{3}\\ [0-9]{2}\\ [0-9]{2})"));
            RuleFor(x => x.Login).Matches(new Regex("[a-zA-Z0-9]")).WithMessage("Login format is incorrect");
            RuleFor(x => x.Password).Matches(new Regex("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$"))
                .WithMessage("Password format is incorrect!");
           */
        }
        public bool teacherCheck(ICollection<Teacher> teacher)
        {
            if (teacher == null) return true;
            bool res = true;
            IEnumerable<int> list = _teacherServices.GetAllAsync().Result.Select(x => x.Id);
            foreach (var item in teacher)
            {
                if (!list.Contains(item.Id)) res = false;
            }
            return res;
        }
    
    }
}
