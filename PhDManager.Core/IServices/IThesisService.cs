using PhDManager.Core.Models;

namespace PhDManager.Core.IServices
{
    public interface IThesisService
    {
        Task<Thesis?> GetThesis(int id);
        Task<List<Thesis>?> GetTheses();
        Task CreateThesis(Thesis thesis);
        Task UpdateThesis(int id, Thesis thesis);
        Task DeleteThesis(int id);
    }
}
