using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MagicVilla_Web.Services
{
    public class BaseService : IBaseService {
        private readonly IHttpClientFactory httpClient;

        public APIResponse ApiResponse { get; set; }
        public BaseService(IHttpClientFactory httpClient) {
            this.httpClient = httpClient;
        }        
        public async Task<T> SendAsync<T>(APIRequest apiRequest) {
            try {
                HttpClient client = httpClient.CreateClient("MagicAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null) {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ApiType) {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                HttpResponseMessage response = null;
                if (!string.IsNullOrEmpty(apiRequest.Token)) {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }
                response = await client.SendAsync(message);
                var apiContent = await response.Content.ReadAsStringAsync();
                var APIRespose = JsonConvert.DeserializeObject<T>(apiContent);
                return APIRespose;
            } catch (Exception ex) {
                APIResponse dto = new() {
                    ErrorMessage = new List<string>() { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponce = JsonConvert.DeserializeObject<T>(res);
                return APIResponce;
            }
        }
    }
}
