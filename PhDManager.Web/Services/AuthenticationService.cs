using System.Security.Claims;

namespace PhDManager.Web.Services
{
    public class AuthenticationService
    {
        public event Action<ClaimsPrincipal>? UserChanged;
        private ClaimsPrincipal? _currentUser;
        public ClaimsPrincipal CurrentUser
        {
            get { return _currentUser ?? new(); }
            set
            {
                _currentUser = value;

                if (UserChanged is not null)
                {
                    UserChanged(_currentUser);
                }
            }
        }
    }
}
