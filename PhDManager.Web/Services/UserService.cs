using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PhDManager.Core.IServices;
using PhDManager.Core.Models;
using PhDManager.Core.ValidationModels;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace PhDManager.Web.Services
{
    public class UserService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationService authenticationService) : IUserService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILocalStorageService _localStorageService = localStorageService;
        private readonly AuthenticationService _authenticationService = authenticationService;

        public async Task<bool> Login(UserLogin userLogin)
        {
            var json = JsonSerializer.Serialize(userLogin);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("user/authenticate", data);

            if (!response.IsSuccessStatusCode) return false;

            string result = await response.Content.ReadAsStringAsync();
            var authResponse = JsonSerializer.Deserialize<AuthResponse>(result);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userLogin.Username),
                new Claim("Token", authResponse.Token)
            }, JwtBearerDefaults.AuthenticationScheme);

            var user = new ClaimsPrincipal(identity);
            _authenticationService.CurrentUser = user;

            return true;
        }
    }
}
