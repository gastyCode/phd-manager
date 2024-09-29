using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PhDManager.Web.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        private readonly AuthenticationService _authenticationService;
        private AuthenticationState _authenticationState;

        public CustomAuthenticationStateProvider(AuthenticationService authenticationService, ProtectedLocalStorage protectedLocalStorage)
        {
            _authenticationState = new AuthenticationState(authenticationService.CurrentUser ?? new());
            _authenticationService = authenticationService;
            _protectedLocalStorage = protectedLocalStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = await GetUser();

            _authenticationService.CurrentUser = user;
            _authenticationState = new AuthenticationState(user);

            return _authenticationState;
        }

        private async Task<ClaimsPrincipal?> GetUser()
        {
            var result = await _protectedLocalStorage.GetAsync<string>("authToken");

            if (!result.Success)
            {
                return new ClaimsPrincipal(new ClaimsIdentity());
            }

            
            var token = result.Value;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            
            if (!IsValid(jsonToken))
            {
                await _protectedLocalStorage.DeleteAsync("authToken");
                return new ClaimsPrincipal(new ClaimsIdentity());
            }

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