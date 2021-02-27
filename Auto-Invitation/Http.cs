using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Auto_Invitation
{
    public class Http
    {

        public static void Initialize(string token)
        {
            UpdateGitHubAuthorization(token);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Core SUper");
        }
        
        public static void UpdateGitHubAuthorization(string value)
            => UpdateAuthorization("token " + value);
        
        public static void UpdateAuthorization(string value)
        {
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", value);
        }
        
        private static HttpClient _httpClient = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(5)
        };
        
        public static async Task<string> GetStrAsync(string url)
        {
            HttpResponseMessage hrm = await _httpClient.GetAsync(url);
            return await hrm.Content.ReadAsStringAsync();
        }

        public static async Task<HttpResponseModel<string>> PostStrAsync(string url, string data)
        {
            var response = await _httpClient.PostAsync(url, new StringContent(data));

            return new HttpResponseModel<string>()
            {
                Status = response.StatusCode,
                Data = response.Content.ReadAsStringAsync().Result,
            };
        }
        
        public static async Task<string> PostStrAsync(string url, string data, int status)
        {
            var response = await _httpClient.PostAsync(url, new StringContent(data));
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}