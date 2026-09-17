using SaatApiCore.DTOs;
using SaatApiCore.Entities;
using SaatApiCore.Interfaces;
using SaatApiDataAccess.Context;
using SaatApiDataAccess.Repositories;

namespace SaatApi.Business.Services;

public class SaatService : GenericRepo<SaatItem>, ISaatService
{
    private readonly ISaatRepository _saatRepository;
    private readonly AppDbContext _appDbContext;
    public SaatService(ISaatRepository saatRepository,AppDbContext context):base(context)
    {
        _saatRepository = saatRepository;
    }

    //public async Task<List<SaatDto>> GetSaatListAsync()
    //{
    //    var saatler = await _saatRepository.GetAllAsync();
    //    return saatler.Select(s => new SaatDto
    //    {
    //        Id = s.Id,
    //        Saat = s.Saat
    //    }).ToList();
    //}

    //public async Task<SaatDto?> GetSaatByIdAsync(int id)
    //{
    //    var item = await _saatRepository.GetByIdAsync(id);
    //    if (item == null) return null;

    //    return new SaatDto
    //    {
    //        Id = item.Id,
    //        Saat = item.Saat
    //    };
    //}

    //public async Task AddSaatAsync(SaatDto saatDto)
    //{
    //    var entity = new SaatItem
    //    {
    //        Saat = saatDto.Saat
    //    };

    //    await _saatRepository.AddAsync(entity);
    //}

    //public async Task UpdateSaatAsync(int id, SaatDto saatDto)
    //{
    //    var entity = await _saatRepository.GetByIdAsync(id);

    //    if (entity == null)
    //    {
    //        throw new KeyNotFoundException("Güncellenecek saat bulunamadı.");
    //    }

    //    entity.Saat = saatDto.Saat;

    //    await _saatRepository.UpdateAsync(entity);
    //}

    //public async Task DeleteSaatAsync(int id)
    //{
    //    await _saatRepository.DeleteAsync(id);
    //}
}