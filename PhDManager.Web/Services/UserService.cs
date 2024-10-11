using Blazored.LocalStorage;
using Newtonsoft.Json;
using PhDManager.Core.IServices;
using PhDManager.Core.Models;
using PhDManager.Core.ValidationModels;
using System.Text;

namespace PhDManager.Web.Services
{
    public class UserService(HttpClient httpClient, ILocalStorageService localStorageService) : IUserService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILocalStorageService _localStorageService = localStorageService;

        public async Task DeleteUser(int id)
        {
            var response = await _httpClient.DeleteAsync($"user/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<User?> GetUser(int id)
        {
            var response = await _httpClient.GetAsync($"user/{id}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<User?>(responseBody);
        }

        public async Task<List<User>?> GetUsers()
        {
            var response = await _httpClient.GetAsync("user");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<User>?>(responseBody);
        }

        public async Task<User?> Login(UserLogin userLogin)
        {
            var json = JsonConvert.SerializeObject(userLogin);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("user/login", data);

            if (!response.IsSuccessStatusCode) return null;

            string result = await response.Content.ReadAsStringAsync();
            var authResponse = JsonConvert.DeserializeObject<AuthResponse>(result);
            var user = authResponse?.User;

            await _localStorageService.SetItemAsync("authToken", authResponse?.Token);
            
            return user;
        }

        public async Task Logout() => await _localStorageService.RemoveItemAsync("authToken");
    }
}
