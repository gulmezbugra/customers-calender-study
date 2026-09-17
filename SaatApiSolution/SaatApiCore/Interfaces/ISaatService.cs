using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaatApiCore.DTOs;
using SaatApiCore.Entities;

namespace SaatApiCore.Interfaces
{
    public interface ISaatService: IGenericRepo<SaatItem>
    {
        //Task<List<SaatDto>> GetSaatListAsync();
        //Task<SaatDto?> GetSaatByIdAsync(int id);
        //Task AddSaatAsync(SaatDto saatDto);
        //Task UpdateSaatAsync(int id, SaatDto saatDto);
        //Task DeleteSaatAsync(int id);
    }
}
