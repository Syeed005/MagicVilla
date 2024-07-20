using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IServices {
    public interface IVillaService {
        Task<T> CreateAsync<T>(VillaDTOCreated dto, string token);
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> UpdateAsync<T>(VillaDTOUpdated dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
