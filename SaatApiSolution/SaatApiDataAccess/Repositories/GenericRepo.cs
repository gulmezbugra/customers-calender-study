using Microsoft.EntityFrameworkCore;
using SaatApiCore.Interfaces;
using SaatApiDataAccess.Context;

namespace SaatApiDataAccess.Repositories;

public class GenericRepo<T> : IGenericRepo<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepo(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, T entity)
    {
        var mevcut = await _dbSet.FindAsync(id);

        if (mevcut == null)
        {
            throw new KeyNotFoundException($"Güncellenecek kayıt bulunamadı: {id}");
        }

        typeof(T).GetProperty("Id")?.SetValue(entity, id);

        _context.Entry(mevcut).CurrentValues.SetValues(entity);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}