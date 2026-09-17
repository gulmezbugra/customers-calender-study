using Microsoft.EntityFrameworkCore;
using SaatApiCore.Entities;
using SaatApiCore.Interfaces;
using SaatApiDataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaatApiDataAccess.Repositories;

public class KayitRepository : GenericRepo<KayitAtama>, IKayitRepository
{
    public KayitRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<KayitAtama>> GetDoorByIdAsync(int id)
    {
        string doorName = $"Kapı-{id}";

        return await _dbSet
            .AsNoTracking()
            .Where(x => x.Door == doorName)
            .ToListAsync();
    }
}

