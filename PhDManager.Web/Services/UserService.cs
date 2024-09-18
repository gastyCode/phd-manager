using PhDManager.Core.IServices;
using PhDManager.Core.Models;
using System.Text;
using System.Text.Json;

namespace PhDManager.Web.Services
{
    public class UserService(HttpClient httpClient) : IUserService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<User?> AuthenticateUser(string username, string password)
        {
            var data = new StringContent(JsonSerializer.Serialize(new { username, password }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("user/authenticate", data);

            return await response.Content.ReadFromJsonAsync<User>();
        }
    }
}
