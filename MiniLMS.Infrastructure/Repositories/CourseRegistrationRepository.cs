using Microsoft.EntityFrameworkCore;
using MiniLMS.Application.Services;
using MiniLMS.Domain.Entities;
using MiniLMS.Infrastructure.DataAccess;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLMS.Infrastructure.Repositories
{
    public class CourseRegistrationRepository : ICourseRegistrationRepository
    {
        private readonly MiniLMSDbContext _context;

        public CourseRegistrationRepository(MiniLMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseRegistration>> GetAllRegistrationsAsync()
        {
            return await _context.CourseRegistrations
                .Include(cr => cr.Student)
                .Include(cr => cr.Course)
                .ToListAsync();
        }

        public async Task<CourseRegistration> GetRegistrationByIdAsync(int id)
        {
            return await _context.CourseRegistrations
                .Include(cr => cr.Student)
                .Include(cr => cr.Course)
                .FirstOrDefaultAsync(cr => cr.Id == id);
        }

        public async Task RegisterStudentAsync(CourseRegistration registration)
        {
            _context.CourseRegistrations.Add(registration);
            await _context.SaveChangesAsync();
        }

        public async Task UnregisterStudentAsync(int id)
        {
            var registration = await _context.CourseRegistrations.FindAsync(id);
            if (registration != null)
            {
                _context.CourseRegistrations.Remove(registration);
                await _context.SaveChangesAsync();
            }
        }
    }

}
