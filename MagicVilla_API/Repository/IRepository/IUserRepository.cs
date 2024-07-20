using MagicVilla_API.DTO;
using MagicVilla_API.Models;

namespace MagicVilla_API.Repository.IRepository {
    public interface IUserRepository {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO);
    }
}
