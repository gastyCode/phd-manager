using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PhDManager.Core.IServices;
using PhDManager.Core.Models;
using PhDManager.Core.ValidationModels;
using System.DirectoryServices;
using System.Security.Claims;
using PhDManager.Api.Data;

namespace PhDManager.Api.Services
{
    public class UserService(AppDbContext context, IOptions<ActiveDirectoryOptions> options) : IUserService
    {
        private readonly AppDbContext _context = context;
        private readonly ActiveDirectoryOptions _options = options.Value;

        public Task<User?> Login(UserLogin userLogin)
        {
            SearchResult result;
            try
            {
                DirectoryEntry entry = new(_options.LdapPath, userLogin.Username, userLogin.Password);
                DirectorySearcher searcher = new(entry);
                searcher.PropertiesToLoad.Add("cn");
                searcher.PropertiesToLoad.Add("givenName");
                searcher.PropertiesToLoad.Add("sn");
                searcher.Filter = $"(&(uid={userLogin.Username}))";
                result = searcher.FindOne();
            }
            catch
            {
                return Task.FromResult<User?>(null);
            }

            var user = _context.Users.FirstOrDefault(user => user.Username == userLogin.Username);
            if (user is null)
            {
                user = new User
                {
                    Username = userLogin.Username,
                    DisplayName = result.Properties["cn"][0].ToString(),
                    FirstName = result.Properties["givenName"][0].ToString(),
                    LastName = result.Properties["sn"][0].ToString(),
                    Role = "User"
                };

                _context.Users.Add(user);
                _context.SaveChanges();
            }

            return Task.FromResult<User?>(user);
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }
    }
}
