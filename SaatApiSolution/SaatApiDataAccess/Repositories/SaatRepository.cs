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

public class SaatRepository : GenericRepo<SaatItem>, ISaatRepository
{
    public SaatRepository(AppDbContext context) : base(context)
    {
    }
}

