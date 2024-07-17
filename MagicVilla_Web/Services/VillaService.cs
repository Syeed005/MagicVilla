using MagicVilla_Utility;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using System.Net.Http.Headers;

namespace MagicVilla_Web.Services {
    public class VillaService : BaseService, IVillaService {
        private readonly IHttpClientFactory httpClient;
        private string villaUrl;

        public VillaService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient) {
            this.httpClient = httpClient;
            this.villaUrl = configuration.GetValue<string>("ServiceUrl:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaDTOCreated dto) {
            return SendAsync<T>(new APIRequest() {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = villaUrl + "/api/villaAPI"
            });
        }

        public Task<T> DeleteAsync<T>(int id) {
            return SendAsync<T>(new APIRequest() {
                ApiType = SD.ApiType.DELETE,
                Url = villaUrl + "/api/villaAPI/" + id
            });
        }

        public Task<T> GetAllAsync<T>() {
            return SendAsync<T>(new APIRequest() {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/villaAPI"
            });
        }

        public Task<T> GetAsync<T>(int id) {
            return SendAsync<T>(new APIRequest() {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/villaAPI/" + id
            });
        }

        public Task<T> UpdateAsync<T>(VillaDTOUpdated dto) {
            return SendAsync<T>(new APIRequest() {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "/api/villaAPI/" + dto.Id
            });
        }
    }
}
