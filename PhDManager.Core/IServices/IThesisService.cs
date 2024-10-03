using PhDManager.Core.Models;

namespace PhDManager.Core.IServices
{
    public interface IThesisService
    {
        Task<Thesis?> GetThesis(int id);
        Task<List<Thesis>?> GetTheses();
        Task<List<Thesis>?> GetThesesByStudent(int studentId);
        Task<List<Thesis>?> GetThesesBySupervisor(int supervisorId);
        Task<List<Thesis>?> GetThesesByStudyProgram(int studyProgramId);
        Task<List<Thesis>?> GetThesesBySubject(int subjectId);
        Task CreateThesis(Thesis thesis);
        Task UpdateThesis(int id, Thesis thesis);
        Task DeleteThesis(int id);
    }
}
