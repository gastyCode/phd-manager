using PhDManager.Core.Models;
using PhDManager.Core.ValidationModels;

namespace PhDManager.Core.IServices
{
    public interface IUserService
    {
        Task<User?> Login(UserLogin userLogin);
        Task Logout();
        Task<User?> GetUser(Guid id);
        Task<List<User>?> GetUsers();
        Task DeleteUser(Guid id);
    }
}
