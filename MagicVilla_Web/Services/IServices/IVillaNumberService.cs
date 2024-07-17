using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IServices {
    public interface IVillaNumberService {
        Task<T> CreateAsync<T>(VillaNumberDTOCreated dto);
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> UpdateAsync<T>(VillaNumberDTOUpdated dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
