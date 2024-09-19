using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PhDManager.Web.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private AuthenticationState _authenticationState;

        public CustomAuthenticationStateProvider(AuthenticationService authenticationService)
        {
            _authenticationState = new AuthenticationState(authenticationService.CurrentUser);

            authenticationService.UserChanged += (newUser) =>
            {
                _authenticationState = new AuthenticationState(newUser);

                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(newUser)));
            };
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(_authenticationState);
        }
    }
}