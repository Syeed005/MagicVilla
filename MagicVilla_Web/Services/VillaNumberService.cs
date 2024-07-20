using MagicVilla_Utility;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace MagicVilla_Web.Services {
    public class VillaNumberService : BaseService, IVillaNumberService {
        private readonly IHttpClientFactory httpClient;
        private string villaUrl;

        public VillaNumberService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient) {
            this.httpClient = httpClient;
            this.villaUrl = configuration.GetValue<string>("ServiceUrl:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaNumberDTOCreated dto, string token) {
            return SendAsync<T>(new APIRequest() {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = villaUrl + "/api/VillaNumberApi",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token) {
            return SendAsync<T>(new APIRequest() {
                ApiType = SD.ApiType.DELETE,
                Url = villaUrl + "/api/VillaNumberApi/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token) {
            return SendAsync<T>(new APIRequest() {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaNumberApi",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token) {
            return SendAsync<T>(new APIRequest() {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaNumberApi/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberDTOUpdated dto, string token) {
            return SendAsync<T>(new APIRequest() {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "/api/VillaNumberApi/" + dto.VillaNo,
                Token = token
            });
        }
    }
}
