using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PhDManager.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace PhDManager.Web.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private AuthenticationState _authenticationState;
        private HttpClient _httpClient;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            _authenticationState = new AuthenticationState(new());
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = await GetUser();

            _authenticationState = new AuthenticationState(user);

            return _authenticationState;
        }

        private async Task<ClaimsPrincipal?> GetUser()
        {
            var token = await _localStorageService.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(token))
            {
                return new ClaimsPrincipal(new ClaimsIdentity());
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            
            if (!IsValid(jsonToken))
            {
                await _localStorageService.RemoveItemAsync("authToken");
                return new ClaimsPrincipal(new ClaimsIdentity());
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var claims = new List<Claim>(new[] {
                    new Claim(ClaimTypes.Name, jsonToken?.Claims.First(claim => claim.Type == "sub").Value),
                    new Claim(ClaimTypes.Role, jsonToken?.Claims.First(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value)
                });

            var identity = new ClaimsIdentity(claims, "jwt");
            return new ClaimsPrincipal(identity);
        }

        private bool IsValid(JwtSecurityToken token)
        {
            var expiration = long.Parse(token?.Claims.First(claim => claim.Type == "exp").Value);
            var expirationDate = DateTimeOffset.FromUnixTimeSeconds(expiration).UtcDateTime;

            return expirationDate >= DateTime.UtcNow;
        }
    }
}