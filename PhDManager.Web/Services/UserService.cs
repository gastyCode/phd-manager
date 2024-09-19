using Blazored.LocalStorage;
using Newtonsoft.Json;
using PhDManager.Core.IServices;
using PhDManager.Core.Models;
using PhDManager.Core.ValidationModels;
using System.Net.Http.Headers;
using System.Text;

namespace PhDManager.Web.Services
{
    public class UserService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationService authenticationService) : IUserService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILocalStorageService _localStorageService = localStorageService;
        private readonly AuthenticationService _authenticationService = authenticationService;

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
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse?.Token);

            return user;
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("authToken");

            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
