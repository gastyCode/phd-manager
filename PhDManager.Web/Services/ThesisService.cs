using Newtonsoft.Json;
using PhDManager.Core.IServices;
using PhDManager.Core.Models;
using System.Text;

namespace PhDManager.Web.Services
{
    public class ThesisService(HttpClient httpClient) : IThesisService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task CreateThesis(Thesis thesis)
        {
            var json = JsonConvert.SerializeObject(thesis);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("thesis", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteThesis(int id)
        {
            var response = await _httpClient.DeleteAsync($"thesis/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<Thesis>?> GetTheses()
        {
            var response = await _httpClient.GetAsync("thesis");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Thesis>?>(responseBody);
        }

        public async Task<List<Thesis>?> GetThesesByStudent(int studentId)
        {
            var response = await _httpClient.GetAsync($"thesis/student/{studentId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Thesis>?>(responseBody);
        }

        public Task<List<Thesis>?> GetThesesByStudyProgram(int studyProgramId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Thesis>?> GetThesesBySubject(int subjectId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Thesis>?> GetThesesBySupervisor(int supervisorId)
        {
            var response = await _httpClient.GetAsync($"thesis/supervisor/{supervisorId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Thesis>?>(responseBody);
        }

        public async Task<Thesis?> GetThesis(int id)
        {
            var response = await _httpClient.GetAsync($"thesis/{id}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Thesis?>(responseBody);
        }

        public async Task UpdateThesis(int id, Thesis thesis)
        {
            var json = JsonConvert.SerializeObject(thesis);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"thesis/{id}", data);
            response.EnsureSuccessStatusCode();
        }
    }
}
