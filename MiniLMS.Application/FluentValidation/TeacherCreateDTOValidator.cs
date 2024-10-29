using FluentValidation;
using MiniLMS.Application.Services;
using MiniLMS.Domain.Entities;
using MiniLMS.Domain.Models.StudentDTO;
using MiniLMS.Domain.Models.TeacherDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiniLMS.Application.FluentValidation
{
    public class TeacherCreateDTOValidator:AbstractValidator<TeacherCreateDTO>
    {
        public IStudentService _studentService;
        public TeacherCreateDTOValidator(IStudentService studentService)
        {
            _studentService = studentService;
            RuleFor(x => x.BirthDate).Must(yearsOldCheck);
            RuleFor(x => x.PhoneNumber).NotEmpty()
           .NotNull().WithMessage("Phone Number is required.")
           .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
           .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
           .Matches(new Regex("(?:\\+[9]{2}[8][0-9]{2}\\ [0-9]{3}\\ [0-9]{2}\\ [0-9]{2})"));
            /////// +99893 557 84 75
            RuleFor(x => x.Login).Matches(new Regex("[a-zA-Z0-9]")).WithMessage("Login format is incorrect");
          //  RuleFor(x => x.Password).Matches(new Regex("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$"))
                //.WithMessage("Password format is incorrect!");

        }
        public bool studentCheck(IEnumerable<int> std)
        {
            if (std == null) return true;
            bool res = true;
            var list = _studentService.GetAllAsync().Result.Select(x=>x.Id);
            foreach (var item in std)
            {
                if (!list.Contains(item)) res = false;
            }
            return res;
        }
        public bool yearsOldCheck(DateOnly? date)
        {
            int dateNow = DateTime.Now.Year;
            if (dateNow - DateTime.Parse(date.ToString()).Year < 18)
            {
                return false;
            }
                return true;
        }
    }
}
