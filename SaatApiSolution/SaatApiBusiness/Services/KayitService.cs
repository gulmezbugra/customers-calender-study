using Microsoft.EntityFrameworkCore;
using SaatApiCore.DTOs;
using SaatApiCore.Entities;
using SaatApiCore.Interfaces;
using SaatApiDataAccess.Context;
using SaatApiDataAccess.Repositories;

namespace SaatApi.Business.Services;

public class KayitService : GenericRepo<KayitAtama>, IKayitService
{
    private readonly IKayitRepository _kayitRepository;
    private readonly AppDbContext _appDbContext;

    public KayitService(IKayitRepository kayitRepository, AppDbContext context) : base(context)
    {
        _kayitRepository = kayitRepository;
    }

    public async Task<List<KayitAtama>> GetDoorByIdAsync(int id)
    {
        string doorName = $"Kapı-{id}";

        return await _dbSet
            .AsNoTracking()
            .Where(x => EF.Property<string>(x, "Door") == doorName)
            .ToListAsync();
    }
}