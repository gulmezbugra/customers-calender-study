using SaatApiCore.Entities;

namespace SaatApiCore.Interfaces
{
    public interface IKayitRepository : IGenericRepo<KayitAtama>
    {
        Task<List<KayitAtama>> GetDoorByIdAsync(int id);
    }
}