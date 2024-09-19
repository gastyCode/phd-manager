using System.Security.Claims;

namespace PhDManager.Web.Services
{
    public class AuthenticationService
    {
        public ClaimsPrincipal? CurrentUser { get; set; }
    }
}
