using MagicVilla_Web.DTO;

namespace MagicVilla_Web.Services.IServices {
    public interface IVillaService {
        Task<T> GetAsync<T>(int id);
        Task<T> GetAllAsync<T>();
        Task<T> CreateAsync<T>(VillaDTOCreated dto);
        Task<T> UpdateAsync<T>(VillaDTOUpdated dto);
        Task<T> DeleteAsync<T>(int id);
        
    }
}
