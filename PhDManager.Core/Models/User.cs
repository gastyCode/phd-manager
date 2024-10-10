namespace PhDManager.Core.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Uid { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime? FirstLogin { get; set; }

        public StudyProgram? StudyProgram { get; set; }
        public Thesis? Thesis { get; set; }
        public List<Subject> Subjects { get; set; } = new List<Subject>();
        public List<Thesis> CreatedTheses { get; set; } = new List<Thesis>();
    }
}
