using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services {
    public class AuthService : BaseService, IAuthService {
        private readonly IHttpClientFactory httpClient;
        private string url;

        public AuthService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient) {
            this.httpClient = httpClient;
            url = configuration.GetValue<string>("ServiceUrl:VillaAPI");
        }

        public Task<T> LoginAsync<T>(LoginRequestDTO obj) {
            return SendAsync<T>(new APIRequest() {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = url + "/api/UsersAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegistrationRequestDTO obj) {
            return SendAsync<T>(new APIRequest() {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = url + "/api/UsersAuth/register"
            });
        }
    }
}
