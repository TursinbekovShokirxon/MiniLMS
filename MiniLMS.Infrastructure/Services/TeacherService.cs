using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MiniLMS.Application.Services;
using MiniLMS.Domain.Entities;
using MiniLMS.Infrastructure.DataAccess;

namespace MiniLMS.Infrastructure.Services;
public class TeacherService : ITeacherService
{
    private readonly MiniLMSDbContext _context;
    private readonly IMemoryCache _cache;
    public TeacherService(MiniLMSDbContext context, IMemoryCache cache)
    {
        _cache=cache;   
        _context = context;
    }

    public async Task<Teacher> CreateAsync(Teacher entity)
    {
         _context.Attach(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int Id)
    {
        Teacher entity = await _context.Teachers.FirstOrDefaultAsync(x=>x.Id==Id);

        if (entity == null)
            return false;

        _context.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }



    public async Task<IEnumerable<Teacher>> GetAllAsync()
    {
        if (_cache.TryGetValue($"Teachers", out List<Teacher> cachedProduct))
            return cachedProduct;

        IEnumerable<Teacher> teachers = _context.Teachers
        .AsNoTracking().AsEnumerable().OrderBy(x => x.Id);
        _cache.Set("Teachers", cachedProduct, TimeSpan.FromMinutes(10));
        return teachers;
    }

    public async Task<Teacher?> GetByIdAsync(int id)
    {
        if (_cache.TryGetValue($"Teacher_{id}", out Teacher cachedProduct))
        {
           return cachedProduct;
        }
        Teacher? teacherEntity = _context.Teachers.FirstOrDefault(x => x.Id == id);
        _cache.Set($"Teacher_{id}",teacherEntity,TimeSpan.FromMinutes(10));
        return teacherEntity;
    }

    public async Task<bool> UpdateAsync(Teacher entity)
    {
        _context.Teachers.Update(entity);
        var executedRows = await _context.SaveChangesAsync();

        return executedRows > 0;
    }
}
