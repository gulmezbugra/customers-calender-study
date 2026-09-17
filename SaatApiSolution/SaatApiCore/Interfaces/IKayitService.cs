using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaatApiCore.DTOs;
using SaatApiCore.Entities;

namespace SaatApiCore.Interfaces
{
    public interface IKayitService : IGenericRepo<KayitAtama>
    {
        Task<List<KayitAtama>> GetDoorByIdAsync(int id);
    }
}