using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MiniLMS.Application.Services;
using MiniLMS.Domain.Entities;
using MiniLMS.Infrastructure.DataAccess;
using System.Security.Cryptography;

namespace MiniLMS.Infrastructure.Services;
public class StudentService : IStudentService
{
    private readonly MiniLMSDbContext _context;
    private readonly Serilog.ILogger _serilog;
    private readonly IMemoryCache _cache;
    public StudentService(MiniLMSDbContext context,Serilog.ILogger logger,IMemoryCache cache)
    {
        _cache = cache;
        _context = context;
        _serilog = logger;
    }

    public async Task<Student> CreateAsync(Student entity)
    {
        _context.Attach(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int Id)
    {
        Student? entity = await _context.Students.FindAsync(Id);
        if (entity == null)
        {
            _serilog.Warning("Not found delete implementation!");
            return false;
        }

        _context.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        if (_cache.TryGetValue($"Students", out List<Student> cachedProduct))
        {
            _serilog.Debug("Get all in cache");
            return cachedProduct;
        }
        _serilog.Debug("Get all async execute...");
        IEnumerable<Student> students = _context.Students
            .AsNoTracking().OrderBy(x=>x.Id).AsEnumerable();
        
        _cache.Set("Students",students,TimeSpan.FromMinutes(10));
        return students;
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        if (_cache.TryGetValue($"Student_{id}", out Student cachedProduct))
        {
            return cachedProduct;
        }
        Student? studentEntity = await _context.Students
            .FirstOrDefaultAsync(x=>x.Id==id);

        if (studentEntity == null)
        {
            _serilog.Warning($"Student implement send null object!");
        }
        _cache.Set($"Student_{id}", studentEntity, TimeSpan.FromMinutes(10));
        return studentEntity;
    }

    public async Task<bool> UpdateAsync(Student entity)
    {
        Console.WriteLine(entity.Login);
        
        _context.Students.Update(entity);
        int executedRows = await _context.SaveChangesAsync();

        return executedRows > 0;
    }
}
