using Microsoft.Extensions.Options;
using PhDManager.Api.Contexts;
using PhDManager.Core.IServices;
using PhDManager.Core.Models;
using System.DirectoryServices;

namespace PhDManager.Api.Services
{
    public class UserService(AppDbContext context, IOptions<ActiveDirectoryOptions> options) : IUserService
    {
        private readonly AppDbContext _context = context;
        private readonly ActiveDirectoryOptions _options = options.Value;

        public Task<User?> AuthenticateUser(string username, string password)
        {
            SearchResult result;
            try
            {
                DirectoryEntry entry = new(_options.LdapPath, username, password);
                DirectorySearcher searcher = new(entry);
                searcher.PropertiesToLoad.Add("cn");
                searcher.PropertiesToLoad.Add("givenName");
                searcher.PropertiesToLoad.Add("sn");
                searcher.Filter = $"(&(uid={username}))";
                result = searcher.FindOne();
            }
            catch
            {
                return Task.FromResult(new User());
            }

            var user = _context.Users.FirstOrDefault(user => user.Upn == username);
            if (user is null)
            {
                user = new User
                {
                    Upn = username,
                    DisplayName = result.Properties["cn"][0].ToString(),
                    FirstName = result.Properties["givenName"][0].ToString(),
                    LastName = result.Properties["sn"][0].ToString(),
                    Role = "User"
                };

                _context.Users.Add(user);
                _context.SaveChanges();
            }

            return Task.FromResult(user);
        }
    }
}
