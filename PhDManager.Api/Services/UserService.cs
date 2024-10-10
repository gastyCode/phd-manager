using Microsoft.Extensions.Options;
using PhDManager.Core.IServices;
using PhDManager.Core.Models;
using PhDManager.Core.ValidationModels;
using LdapForNet;
using PhDManager.Api.Data;
using static LdapForNet.Native.Native;
using Microsoft.EntityFrameworkCore;

namespace PhDManager.Api.Services
{
    public class UserService(AppDbContext context, IOptions<ActiveDirectoryOptions> options) : IUserService
    {
        private readonly AppDbContext _context = context;
        private readonly ActiveDirectoryOptions _options = options.Value;

        public async Task DeleteUser(Guid id)
        {
            var user = await GetUser(id);

            if (user is null) return;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUser(Guid id) => await _context.Users.SingleOrDefaultAsync(u => u.UserId == id);

        public async Task<List<User>?> GetUsers() => await _context.Users.ToListAsync();

        public async Task<User?> Login(UserLogin userLogin)
        {
            IEnumerable<LdapEntry> entries;

            try
            {
                using var connection = new LdapConnection();

                connection.Connect(_options.LdapPath, 389, LdapSchema.LDAP);
                connection.SetOption(LdapOption.LDAP_OPT_REFERRALS, IntPtr.Zero);
                await connection.BindAsync(LdapAuthType.Simple, new LdapCredential
                {
                    UserName = $"{userLogin.Username}@{_options.LdapPath}",
                    Password = userLogin.Password
                });

                entries = await connection.SearchAsync("dc=fri,dc=uniza,dc=sk", $"(uid={userLogin.Username})");
            }
            catch (LdapException)
            {
                return null;
            }

            var result = entries?.FirstOrDefault();
            if (result is null) return null;

            var user = _context.Users.FirstOrDefault(user => user.Username == userLogin.Username);
            if (user is null)
            {
                user = new User
                {
                    Uid = result.DirectoryAttributes["uidNumber"].GetValue<string>(),
                    Username = userLogin.Username,
                    DisplayName = result.DirectoryAttributes["cn"].GetValue<string>(),
                    FirstName = result.DirectoryAttributes["givenName"].GetValue<string>(),
                    LastName = result.DirectoryAttributes["sn"].GetValue<string>(),
                    Role = nameof(Role.Student),
                    FirstLogin = DateTime.UtcNow
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            return user;
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }
    }
}
