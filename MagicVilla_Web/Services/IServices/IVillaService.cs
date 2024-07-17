using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IServices {
    public interface IVillaService {
        Task<T> CreateAsync<T>(VillaDTOCreated dto);
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> UpdateAsync<T>(VillaDTOUpdated dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
