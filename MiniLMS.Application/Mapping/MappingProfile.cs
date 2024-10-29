using AutoMapper;
using MiniLMS.Domain.Entities;
using MiniLMS.Domain.Models.CourseDTO;
using MiniLMS.Domain.Models.CourseRegistrationDTO;
using MiniLMS.Domain.Models.StudentDTO;
using MiniLMS.Domain.Models.TeacherDTO;
namespace MiniLMS.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region CourseRegistration
            // Настройка для CourseRegistration
            CreateMap<CourseRegistration, CourseRegistrationAllDTO>().ReverseMap();
            #endregion

            #region Course  
            // Настройка для Course
            CreateMap<Course, CourseAllDTO>().ReverseMap();

            #endregion

            #region Student 
            // Student Mappings
            CreateMap<StudentBaseDTO, StudentCreateDTO>().
                ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber)).
                ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName)).
                ForMember(dest => dest.Major, opt => opt.MapFrom(src => src.Major));

            CreateMap<Student, StudentCreateDTO>().
                ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber)).
                ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName)).
                ForMember(dest => dest.Major, opt => opt.MapFrom(src => src.Major)).
                ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate)).
                ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender)).
                ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login)).
                ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password)).ReverseMap();

            CreateMap<Student, StudentGetDTO>().
               ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber)).
               ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName)).
               ForMember(dest => dest.Major, opt => opt.MapFrom(src => src.Major)).
               ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate)).
               ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender)).
               ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login)).ReverseMap();

            CreateMap<Student, StudentGetDTO>().ReverseMap();
            CreateMap<Student, UpdateStudentDTO>().ReverseMap();
            CreateMap<StudentCreateDTO, StudentGetDTO>().ReverseMap();
            CreateMap<StudentCreateDTO, UpdateStudentDTO>().ReverseMap();
            #endregion

            #region Teacher
            // Teacher Mappings
            CreateMap<TeacherBaseDTO, TeacherCreateDTO>().ReverseMap();
            CreateMap<TeacherBaseDTO, TeacherGetDTO>().ReverseMap();
            CreateMap<TeacherBaseDTO, UpdateTeacherDTO>().ReverseMap();
            CreateMap<TeacherCreateDTO, TeacherGetDTO>().ReverseMap();
            CreateMap<TeacherCreateDTO, UpdateTeacherDTO>().ReverseMap();
            #endregion

        }
    }
}
