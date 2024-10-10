using Microsoft.EntityFrameworkCore;
using PhDManager.Api.Data;
using PhDManager.Core.IServices;
using PhDManager.Core.Models;

namespace PhDManager.Api.Services
{
    public class ThesisService(AppDbContext context) : IThesisService
    {
        private readonly AppDbContext _context = context;

        public async Task CreateThesis(Thesis thesis)
        {
            _context.Theses.Add(thesis);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteThesis(Guid id)
        {
            var thesis = await GetThesis(id);

            if (thesis is null) return;

            _context.Theses.Remove(thesis);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Thesis>?> GetTheses() => await _context.Theses.ToListAsync();

        public async Task<List<Thesis>?> GetThesesByStudent(Guid studentId) => await _context.Theses.Where(t => t.Student.UserId == studentId).ToListAsync();

        public Task<List<Thesis>?> GetThesesByStudyProgram(Guid studyProgramId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Thesis>?> GetThesesBySubject(Guid subjectId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Thesis>?> GetThesesBySupervisor(Guid supervisorId) => await _context.Theses.Where(t => t.Supervisor.UserId == supervisorId).ToListAsync();

        public async Task<Thesis?> GetThesis(Guid id) => await _context.Theses.SingleOrDefaultAsync(t => t.ThesisId == id);

        public async Task UpdateThesis(Guid id, Thesis thesis)
        {
            var oldThesis = await GetThesis(id);

            if (oldThesis is null) return;

            _context.Entry(oldThesis).CurrentValues.SetValues(thesis);
        }
    }
}
