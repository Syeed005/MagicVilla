using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services.IServices
{
    public interface IBaseService
    {
        APIResponse ApiResponse { get; set; }

        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}