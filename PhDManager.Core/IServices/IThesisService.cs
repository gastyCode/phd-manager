using PhDManager.Core.Models;

namespace PhDManager.Core.IServices
{
    public interface IThesisService
    {
        Task<Thesis?> GetThesis(Guid id);
        Task<List<Thesis>?> GetTheses();
        Task<List<Thesis>?> GetThesesByStudent(Guid studentId);
        Task<List<Thesis>?> GetThesesBySupervisor(Guid supervisorId);
        Task<List<Thesis>?> GetThesesByStudyProgram(Guid studyProgramId);
        Task<List<Thesis>?> GetThesesBySubject(Guid subjectId);
        Task CreateThesis(Thesis thesis);
        Task UpdateThesis(Guid id, Thesis thesis);
        Task DeleteThesis(Guid id);
    }
}
