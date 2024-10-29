using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniLMS.Application.Mapping;
using MiniLMS.Application.Services;
using MiniLMS.Infrastructure.DataAccess;
using MiniLMS.Infrastructure.Repositories;
using MiniLMS.Infrastructure.Services;

namespace MiniLMS.Infrastructure;
public static class ConfigureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<CourseService>();
        services.AddScoped<ICourseRegistrationRepository, CourseRegistrationRepository>();
        services.AddTransient<CourseRegistrationService>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddDbContext<MiniLMSDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("MiniLMSDbConnection")));
    }
}
